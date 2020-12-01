# 指定变量环境
export ASPNETCORE_ENVIRONMENT=Production

# 安装 dotnet-ef
dotnet tool install -g dotnet-ef

# 更新 dotnet-ef
dotnet tool update -g dotnet-ef

# 数据迁移
dotnet ef migrations add Initial
dotnet ef database update

dotnet ef migrations add -c ConfigurationDbContext Config
dotnet ef database update -c ConfigurationDbContext

dotnet ef migrations add -c PersistedGrantDbContext Grants
dotnet ef database update -c PersistedGrantDbContext