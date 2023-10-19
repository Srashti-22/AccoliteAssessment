using AccoliteAssessment.Persistence.Context;
using AccoliteAssessment.Persistence.Repositories.User;
using AccoliteAssessment.Persistence.Repositories.UserAccount;
using AccoliteAssessment.Core;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AccoliteAssessment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<BankingContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database"), (builder) => builder.MigrationsAssembly(typeof(Program).Assembly.ToString())));
            builder.Services.AddTransient<IUOW, UnitOfWork>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IUserAccountRepository, UserAccountRepository>();
            builder.Services.AddMediatR(assembly => assembly.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<BankingContext>();
                db.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}