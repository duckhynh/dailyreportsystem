# 📊 Daily Report System (SaaS Enterprise)

Một hệ thống Quản lý Báo cáo Hằng ngày toàn diện được xây dựng dựa trên tiêu chuẩn kiến trúc C# .NET Enterprise. Ứng dụng tích hợp Core Web API siêu tốc, bảo mật JWT Authentication, kết hợp với giao diện Dashboard **Minimalist Razor Pages** mang phong cách SaaS mạnh mẽ.

## ✨ Tính năng nổi bật

- 🛡️ **Bảo mật chuẩn hóa:** API được khóa an toàn bởi JWT Bearer Token.
- 📐 **Clean Architecture:** Chia tầng chuẩn xác (Core, Application, Infrastructure, WebApi) giúp mở rộng dự án không giới hạn.
- 🏭 **Generic Repository Pattern:** Tái sử dụng tối đa code, loại bỏ sự lặp lại khi truy vấn Entity Framework Core.
- 💻 **Giao diện đúc sẵn (Razor SPA):** Tích hợp ứng dụng Web (Client-side) chạy bằng Vanilla Javascript gọi trực tiếp Swagger API, thiết kế giao diện Minimalist chuyên nghiệp.
- 🏢 **Xác thực tự động (Business Validation):** Các quy tắc nghiệp vụ khắt khe như "Nhân viên phải được gán vào dự án mới có quyền nộp Báo cáo" được bắt gọn trong Service và Middleware dội thẳng ra giao diện.

## 🛠️ Công nghệ sử dụng

- **Backend Framework:** .NET 10 (C#) / ASP.NET Core Web API
- **Kiến trúc:** Clean Architecture & CQRS-like Services
- **Cơ sở dữ liệu:** SQL Server (sử dụng Entity Framework Core)
- **Frontend Container:** ASP.NET Core Razor Pages + Vanilla JS (LocalStorage + Fetch API)
- **Tài liệu API:** Swagger UI / OpenAPI 3

## 🏗️ Cấu trúc hệ thống

Dự án được phân chia thành 4 lớp cốt lõi:
1. `DailyReportSystem.Core`: Lõi chứa Entities (`User`, `Project`, `DailyReport`), Enums và Interfaces.
2. `DailyReportSystem.Application`: Lớp dịch vụ chứa Business Logic, DTOs và JWT Auth Generation.
3. `DailyReportSystem.Infrastructure`: Lớp quản lý DbContext và kết nối CSDL (Repositories).
4. `DailyReportSystem.WebApi`: Endpoints Controllers, File tĩnh (HTML/CSS) và cầu nối gởi API.

## 🚀 Hướng dấn khởi chạy dự án

### 1. Yêu cầu hệ thống
- Cài đặt .NET SDK mới nhất.
- SQL Server đang chạy ngầm trên `localhost` (Tài khoản mặc định: `sa`, mật khẩu `12345`). Nếu máy bạn dùng cài đặt khác, hãy đổi chuỗi `DefaultConnection` trong file `appsettings.json`.

### 2. Khởi chạy
Mở Terminal / PowerShell và gõ lệnh sau để khởi động chế độ hot-reload:
```bash
cd DailyReportSystem.WebApi
dotnet watch run
```
Ngay sau khi hệ thống gạch nối chạy, Entity Framework sẽ tự động kiểm tra, thiết lập `DailyReportDb` và tạo dữ liệu mồi (Seed Data) chuẩn vào SQL Server.

### 3. Trải nghiệm hệ thống Frontend tích hợp
Sau khi Terminal chạy báo cổng kết nối (VD: `http://localhost:5000`), hệ thống sẽ mở ra giao diện Dashboard.
Bạn nhấp vào **Đăng nhập** với thông tin mặc định:
- **Tài khoản:** `testuser`
- **Mật khẩu:** `12345`

Sau đó, Token sẽ được lưu lại, giúp bạn tùy ý nộp các báo cáo hàng ngày (Khuyên dùng thử `ProjectId` = `1`) và đọc lịch sử báo cáo ngay tại Website mà không cần qua app ngoài hay Postman. Nếu muốn ngắm trực tiếp API Endpoints, hãy tới đường dẫn `/swagger`.

---
*Phát triển bởi [duckhynh](https://github.com/duckhynh)*
