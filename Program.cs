using Microsoft.EntityFrameworkCore;
using WebApi_test.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// サービスの追加
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Swagger/OpenAPIの設定（開発環境のみ）
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORSポリシーの設定
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policyBuilder =>
        {
            policyBuilder.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader();
        });
});

// データベースコンテキストの設定（SQL Server）
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 開発環境でのSwagger設定
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPSリダイレクト
app.UseHttpsRedirection();

// CORSポリシーの適用
app.UseCors("AllowAll");

// 認証ミドルウェア（必要に応じて）
app.UseAuthorization();

// コントローラーのマッピング
app.MapControllers();

// アプリケーションの実行
app.Run();
