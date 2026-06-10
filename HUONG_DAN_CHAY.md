# HƯỚNG DẪN CÀI ĐẶT VÀ CHẠY DỰ ÁN - HỆ THỐNG TUYỂN SINH TRỰC TUYẾN

## 📋 Giới thiệu
Dự án **AdmissionWeb** là hệ thống tuyển sinh trực tuyến cho trường THPT Kiến Trúc, xây dựng trên nền tảng ASP.NET Core 8.0 MVC với MySQL.

### Tính năng chính:
- **Trang chủ**: Hiển thị tin tức, kỳ tuyển sinh đang mở
- **Kỳ tuyển sinh**: Xem danh sách, chi tiết ngành/lớp, chỉ tiêu
- **Nộp hồ sơ**: Đăng ký hồ sơ tuyển sinh trực tuyến (yêu cầu đăng nhập)
- **Tra cứu kết quả**: Tra cứu trạng thái hồ sơ và điểm thi
- **Quản trị Admin**: Dashboard thống kê, duyệt/từ chối hồ sơ

---

## 🛠 Yêu cầu hệ thống

### Bắt buộc:
1. **.NET 8 SDK** (hoặc mới hơn)
   - Tải tại: https://dotnet.microsoft.com/download/dotnet/8.0
   - Chọn **SDK** (không phải Runtime)
   - Sau khi cài, mở Terminal mới và chạy: `dotnet --version` để xác nhận

2. **MySQL Server** (hoặc XAMPP/WAMP)
   - Tải MySQL Server: https://dev.mysql.com/downloads/mysql/
   - Hoặc cài đặt qua XAMPP: https://www.apachefriends.org/

### Tùy chọn (khuyến nghị):
- **Visual Studio 2022** (Community Edition miễn phí)
  - Cài workload "ASP.NET and web development"
- Hoặc **Visual Studio Code** + C# Dev Kit extension

---

## 🚀 Hướng dẫn chạy

### Cách 1: Chạy bằng Command Line (dotnet CLI)

#### Bước 1: Mở Terminal (PowerShell hoặc Command Prompt)
```
cd "C:\Users\sonb1\Downloads\WebTuyenSinh_SourceCode_Complete\AdmissionWeb\AdmissionWeb"
```

#### Bước 2: Khôi phục NuGet packages
```
dotnet restore
```

#### Bước 3: Build dự án
```
dotnet build
```

#### Bước 4: Chạy ứng dụng
```
dotnet run
```

#### Bước 5: Mở trình duyệt
Truy cập: **http://localhost:5157**

> **Lưu ý**: Database sẽ được tự động tạo và seed dữ liệu mẫu khi chạy lần đầu tiên (thông qua `DbInitializer`).

---

### Cách 2: Chạy bằng Visual Studio 2022

1. Mở file **AdmissionWeb.csproj** hoặc solution file bằng Visual Studio 2022
2. Đợi Visual Studio restore NuGet packages tự động
3. Nhấn **F5** (hoặc **Ctrl+F5** để chạy không debug)
4. Trình duyệt sẽ tự động mở

---

## 🔑 Tài khoản mặc định

### Admin (Quản trị viên)
| Thông tin | Giá trị |
|-----------|---------|
| **Email** | `admin@admission.edu.vn` |
| **Mật khẩu** | `Admin@123` |
| **Role** | Admin |

> Đăng nhập bằng tài khoản Admin để truy cập trang quản trị tại: `/Admin/Dashboard`

### Đăng ký tài khoản mới
- Truy cập trang **Đăng ký** từ thanh điều hướng
- Tài khoản mới sẽ có role **Candidate** (Thí sinh)
- Mật khẩu tối thiểu 6 ký tự

---

## 📂 Cấu trúc dự án

```
AdmissionWeb/
├── Areas/
│   └── Admin/                          # Khu vực quản trị
│       ├── Controllers/
│       │   ├── DashboardController.cs   # Dashboard thống kê
│       │   └── ApplicationController.cs # Quản lý hồ sơ
│       └── Views/
│           ├── Dashboard/Index.cshtml   # Trang dashboard
│           ├── Application/
│           │   ├── Index.cshtml         # Danh sách hồ sơ
│           │   └── Details.cshtml       # Chi tiết & duyệt hồ sơ
│           └── Shared/_AdminLayout.cshtml
├── Controllers/
│   ├── HomeController.cs               # Trang chủ
│   ├── AdmissionController.cs          # Kỳ tuyển sinh & Tin tức
│   ├── ApplicationController.cs        # Nộp hồ sơ & Tra cứu
│   └── ResultController.cs             # Tra cứu kết quả
├── Data/
│   ├── ApplicationDbContext.cs          # Database context (EF Core)
│   └── SeedData/DbInitializer.cs       # Khởi tạo dữ liệu mẫu
├── Models/
│   ├── Entities/                        # Entity models
│   │   ├── AdmissionPeriod.cs           # Kỳ tuyển sinh
│   │   ├── Application.cs              # Hồ sơ đăng ký
│   │   ├── ProgramOption.cs            # Ngành/Lớp
│   │   ├── ExamResult.cs               # Kết quả thi
│   │   ├── NewsArticle.cs              # Tin tức
│   │   └── ContactRequest.cs           # Liên hệ
│   ├── Identity/ApplicationUser.cs      # User mở rộng Identity
│   └── ViewModels/                      # View models
├── Services/
│   ├── Interfaces/                      # Service interfaces
│   └── Implementations/                 # Service implementations
├── Repositories/
│   ├── Interfaces/IRepository.cs        # Generic repository interface
│   └── Implementations/Repository.cs    # Generic repository
├── Views/
│   ├── Home/Index.cshtml               # Trang chủ
│   ├── Admission/
│   │   ├── Index.cshtml                # Danh sách kỳ tuyển sinh
│   │   ├── Detail.cshtml               # Chi tiết kỳ tuyển sinh
│   │   └── News.cshtml                 # Chi tiết tin tức
│   ├── Application/
│   │   ├── Register.cshtml             # Form đăng ký hồ sơ
│   │   ├── MyApplications.cshtml       # Hồ sơ của tôi
│   │   └── Status.cshtml              # Tra cứu trạng thái
│   ├── Result/Search.cshtml            # Tra cứu kết quả
│   └── Shared/
│       ├── _Layout.cshtml              # Layout chính
│       ├── _LoginPartial.cshtml        # Thanh đăng nhập
│       └── _ValidationScriptsPartial.cshtml
├── wwwroot/                            # Static files (CSS, JS, images)
├── Program.cs                          # Entry point + DI config
├── appsettings.json                    # Cấu hình ứng dụng
└── AdmissionWeb.csproj                 # Project file
```

---

## 🗄 Cấu hình Database

### Connection String mặc định (MySQL):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=AdmissionWebDb;User=root;Password=your_password;"
  }
}
```

Mở file `appsettings.json` và sửa `DefaultConnection` với cấu hình MySQL của bạn. Đảm bảo thay đổi `User` và `Password` cho đúng.

> **Lưu ý quan trọng**: Dự án sử dụng `EnsureCreated()` nên database sẽ được tạo tự động khi chạy lần đầu. KHÔNG cần chạy migration thủ công.

---

## 🧭 Hướng dẫn sử dụng

### Dành cho Thí sinh (Candidate)
1. **Đăng ký tài khoản** → Đăng nhập
2. Vào **Kỳ tuyển sinh** → Chọn kỳ tuyển sinh → **Đăng ký hồ sơ**
3. Điền đầy đủ thông tin → **Gửi hồ sơ**
4. Nhận mã hồ sơ (VD: `TS202606101234`)
5. Vào **Hồ sơ của tôi** hoặc **Tra cứu kết quả** để theo dõi

### Dành cho Admin
1. Đăng nhập bằng tài khoản Admin
2. Nhấn link **Quản trị** trên thanh điều hướng
3. **Dashboard**: Xem thống kê tổng quan
4. **Quản lý hồ sơ**: Xem danh sách, duyệt/từ chối hồ sơ
5. Click **Chi tiết** → Xem thông tin → Chọn trạng thái → **Cập nhật**

---

## ⚠️ Xử lý lỗi thường gặp

### 1. Lỗi "Cannot connect to database"
- Kiểm tra MySQL service đã được bật chưa (nếu dùng XAMPP thì bật module MySQL)
- Kiểm tra lại username, password, port trong connection string

### 2. Lỗi "dotnet not found"
- Cài .NET 8 SDK từ https://dotnet.microsoft.com/download
- Khởi động lại Terminal sau khi cài

### 3. Lỗi NuGet packages
```
dotnet restore --force
dotnet build
```

### 4. Port 5157 đã bị sử dụng
Mở `Properties/launchSettings.json`, đổi port trong `applicationUrl`

---

## 📧 Thông tin công nghệ sử dụng

| Công nghệ | Phiên bản |
|------------|-----------|
| ASP.NET Core MVC | 8.0 |
| Entity Framework Core | 8.0.11 |
| ASP.NET Core Identity | 8.0.11 |
| MySQL | 8.x |
| Bootstrap | 5.x |
| Font Awesome | 6.x |
| jQuery | 3.x |
| MailKit | 4.17.0 |

---

**© 2026 - Hệ thống Tuyển sinh Trường THPT Kiến Trúc**
