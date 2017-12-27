# Multitenant app with separated database demo

Ý tưởng chủ đạo là DbContext có scoped lifecycle, tức là chỉ sống trong 1 request roundtrip, nên có thể lấy connection string từ thông tin tenant gửi request đó để khởi tạo dbContext. Thay vì config connection string trong option pass vào lúc start up, mình sẽ set connection string trong method OnConfiguring

```c#
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    var connectionString = _tenant.ConnectionString;
    optionsBuilder.UseNpgsql(connectionString);
    base.OnConfiguring(optionsBuilder);
}
```

Tenant được get từ httpcontext, thông qua `ITenantResolver`. Ở đây chỉ đơn giản dựa vào host name.

```c#
// ApplicationDbContext.cs
public ApplicationDbContext(ITenantResolver tenantResolver)
{
    _tenant = tenantResolver.Resolve().GetAwaiter().GetResult();
}
```

```c#
// TenantResolver.cs
public async Task<AppTenant> Resolve()
{
    var hostname = _httpContextAccessor.HttpContext.Request.Host.Value;
    var tenant = await _tenantCatalog.Get(t => t.HostName == hostname);
    return tenant;
}
```

TenantCatalog cũng đọc từ file appsettings ra, assuming là 1 DbSet nào đó.

Như vậy có vài vấn đề hay ho phát sinh:

- TenantResolver được gọi rất nhiều lần, mà thường là toàn kết quả giống nhau, nếu catalog là db thật thì sẽ rất chậm => cần có chiến lược cache cho thằng này. (1)
- Để giảm các request resolve tenant ở (1) Sẽ có nhiều service được sử dụng chung cho toàn bộ ternant, khác với singleton là dùng cho cả app, hay scoped là dùng cho mỗi request, cần có chiêu thức nào đó tạo ra các service như này.
- Có cách nào resolve tenant hay hơn là dựa vào hostname không nhỉ?

Có vẻ giải quyết được vấn đề dbcontext, nhưng thằng openid cũng dựa vào domain để xác định issuer, và từ đó validate 1 token, nên không biết có chết không? Cần nghiên cứu thêm
