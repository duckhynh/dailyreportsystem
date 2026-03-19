# Phân tích & Thiết kế Hệ thống Báo cáo Công việc (Daily Report System)

Dưới đây là tài liệu phân tích và thiết kế chi tiết dựa trên yêu cầu của bạn. Tôi đã cập nhật và sửa lại cấu trúc các biểu đồ Mermaid để đảm bảo hiển thị không bị lỗi.

---

## 1. Phân tích Functional Requirement (Yêu cầu chức năng)

### 1.1 Yêu cầu phía User (Người dùng thường)
*   **Đăng nhập (Login):** Người dùng sử dụng tài khoản được Admin cung cấp.
*   **Bảng điều khiển (Sidebar):**
    *   **Thông tin cá nhân:** Xem thông tin cá nhân và quản lý mật khẩu.
    *   **Báo cáo công việc:** Màn hình chính để gửi báo cáo hằng ngày.
    *   **Lịch sử báo cáo:** Màn hình phân trang liệt kê các báo cáo đã nộp, bộ lọc theo ngày và theo danh mục dự án.
    *   **Đăng xuất:** Kết thúc phiên làm việc.
*   **Quy trình báo cáo (Report Flow):**
    1.  Chọn dự án từ danh sách các dự án tham gia.
    2.  Hôm nay đã làm gì? (Today's tasks) - Textarea.
    3.  Gặp khó khăn gì? (Issues/Blockers) - Textarea.
    4.  Ngày mai sẽ làm gì? (Tomorrow's plan) - Textarea.
    5.  Nhấn Submit (Lưu dữ liệu).

### 1.2 Yêu cầu phía Admin (Quản trị viên)
*   **Quản lý Tài khoản (User):** Tạo tài khoản, khóa tài khoản nhân viên đã nghỉ, sửa thông tin.
*   **Quản lý Dự án (Project):** Tạo dự án mới, phân công (Assign) User vào dự án để họ có thể chọn khi báo cáo.
*   **Xem báo cáo hệ thống:** Xem toàn bộ các báo cáo của User trên hệ thống, lọc thông minh theo tên User, tên Dự án, hoặc theo Ngày cụ thể.

---

## 2. Phân tích Non-Functional Requirement (Yêu cầu phi chức năng)

*   **Bảo mật:** Mật khẩu Hash (bcrypt), Auth bảo mật bằng JWT Token (JSON Web Token).
*   **Hiệu suất:** Response time dưới 1s, sử dụng cơ chế kéo phân trang (Pagination) để hiển thị danh sách dài tránh làm treo trình duyệt.
*   **Tính khả dụng:** Giao diện Tương thích (Responsive) hoạt động tốt trên Desktop lẫn Mobile (để tiện lợi báo cáo lúc về).
*   **Khả năng bảo trì:** Sử dụng Clean Architecture, tách tầng rành mạch giúp dễ bảo trì lâu dài.

---

## 3. Usecase Diagram (Biểu đồ Luồng sử dụng)

```mermaid
flowchart TD
    US([👤 Người dùng])
    AD([👑 Quản trị viên])

    subgraph System ["Hệ thống Báo cáo (Daily Report)"]
        UC1("Đăng nhập / Đăng xuất")
        UC2("Xem / Sửa thông tin cá nhân")
        UC3("Tạo báo cáo công việc")
        UC4("Xem lịch sử báo cáo cá nhân")
        
        UC5("Quản lý User")
        UC6("Quản lý Dự án & Assign User")
        UC7("Quản lý & Xem Toàn bộ Báo cáo")
        
        UC3_1("🔹 Chọn Dự án")
        UC3_2("🔹 Ghi chú: Đã làm gì")
        UC3_3("🔹 Ghi chú: Khó khăn")
        UC3_4("🔹 Ghi chú: Kế hoạch ngày mai")
    end

    US --> UC1
    US --> UC2
    US --> UC3
    US --> UC4

    AD --> UC1
    AD --> UC5
    AD --> UC6
    AD --> UC7

    UC3 -.->|Bao gồm luồng| UC3_1
    UC3 -.->|Bao gồm luồng| UC3_2
    UC3 -.->|Bao gồm luồng| UC3_3
    UC3 -.->|Bao gồm luồng| UC3_4
```

---

## 4. Class Diagram (Biểu đồ Lớp dữ liệu)

```mermaid
classDiagram
    class User {
        +int Id
        +string Username
        +string PasswordHash
        +string FullName
        +string Email
        +RoleType Role
    }

    class Project {
        +int Id
        +string ProjectName
        +string Description
        +DateTime CreatedAt
    }

    class UserProject {
        +int UserId
        +int ProjectId
        +DateTime AssignedDate
    }

    class DailyReport {
        +int Id
        +int UserId
        +int ProjectId
        +DateTime ReportDate
        +string TasksDone
        +string Issues
        +string TomorrowPlan
        +DateTime CreatedAt
    }

    User "1" --> "*" DailyReport : Creates
    Project "1" --> "*" DailyReport : Belongs To
    User "1" --> "*" UserProject : Assigned To
    Project "1" --> "*" UserProject : Includes
```

---

## 5. Sequence Diagram (Luồng Gửi Báo Cáo Công Việc)

```mermaid
sequenceDiagram
    actor U as User
    participant UI as Web/App UI
    participant C as ReportController
    participant S as ReportService
    participant DB as Database

    U->>UI: Mở tab "Báo cáo công việc"
    UI->>C: GET /api/projects/my-projects
    C->>S: GetUserProjects(userId)
    S->>DB: Truy vấn CSDL
    DB-->>S: Trả danh sách Project
    S-->>C: Data Models
    C-->>UI: Hiển thị giao diện Form Chọn Dự án (JSON)
    
    U->>UI: Điền thông tin, Bấm "Gửi"
    UI->>C: POST /api/reports (Kèm Request DTO)
    C->>S: CreateReport(DTO)
    
    alt Validation Lỗi (Thiếu dữ liệu)
        S-->>C: Bắn ra Lỗi (Missing Require Fields)
        C-->>UI: HTTP 400 Bad Request
        UI-->>U: Hiển thị cảnh báo màu đỏ
    else Hợp lệ (Thành công)
        S->>DB: Thực thi Save(DailyReport entity)
        DB-->>S: Lưu thành công, sinh ID mới
        S-->>C: HTTP 201 Created
        C-->>UI: Status 201 Success
        UI-->>U: Hiển thị Dialog Khẳng định & Refresh trang
    end
```

---

## 6. Package Diagram (Biểu đồ Phân Lớp Kiến Trúc)

```mermaid
flowchart TD
    subgraph UI ["Presentation Layer (Client)"]
        SPA["React / Vue / Angular"]
    end

    subgraph WEBAPI ["API Layer (Controllers)"]
        Controllers["Controllers (Auth, User, Project, Report)"]
    end

    subgraph BLL ["Business Logic Layer (Services)"]
        Services["Logic Xử Lý & Xác Thực (Validation)"]
        DTOs["Data Transfer Objects (DTO)"]
    end

    subgraph DAL ["Data Access Layer"]
        Repos["Repositories (Trung gian gọi DB)"]
        EF["ORM (Entity Framework / TypeORM)"]
    end

    subgraph DB ["Database System"]
        SQL[(PostgreSQL / MySQL / SQL Server)]
    end

    UI -- "REST API (HTTP calls)" --> WEBAPI
    WEBAPI --> BLL
    BLL --> DAL
    DAL --> DB
```

---

## 7. Deployment Diagram (Biểu đồ Triển Khai)

```mermaid
flowchart TD
    subgraph Client ["Thiết bị Client"]
        B["Trình duyệt (PC, Mobile, Tablet)"]
    end

    subgraph Server ["Web / App Server"]
        Proxy{"Reverse Proxy (Nginx / IIS)"}
        API["Backend Service (.NET, Spring Boot)"]
    end

    subgraph DBNode ["Database Server"]
        Database[("Hệ Quản trị CSDL (SQL)")]
    end

    Client -- "HTTPS / TLS" <--> Proxy
    Proxy <--> API
    API -- "TCP (DB Port)" <--> Database
```

---

## 8. Design Pattern Khuyến Nghị

*   **Repository Pattern:** Phân tách hoàn toàn logic làm việc với CSDL ra khỏi Service. Mọi thao tác đều làm việc qua Interface `IUserRepository`, `IReportRepository`. Tránh việc Service gọi Hardcode vào DB.
*   **Dependency Injection (DI):** Đảo ngược điều khiển, quản lý trọn vẹn và tiêm nội dung thực thi (Service, Repo) tự động vào Controller khi chạy. 
*   **DTO (Data Transfer Object):** Class chỉ chứa dữ liệu để truyền qua lại thông qua các API, không gửi phơi bày Object DB chính ra ngoài mạng internet.

---

## 9. Source Code Template (Kiến trúc C# .NET Minimal Clean Architecture) 

```text
MyApp.DailyReport/
│
├── 1. Core/                            # Tầng Trong Cùng Core
│   ├── Entities/                       # (Model Database: User, Project, DailyReport...)
│   ├── Enums/                          # (Role...)
│   └── Interfaces/                     # (IUserRepository, IReportRepository...)
│
├── 2. Application/                     # Tầng Application Group
│   ├── DTOs/                           # (ReportCreateDTO, UserResponseDTO...)
│   └── Services/                       # (ReportService, AuthService...)
│
├── 3. Infrastructure/                  # Tầng Gọi Ra Ngoài
│   ├── AppDbContext.cs                 # Lớp Entity Framework Core DB Context
│   └── Repositories/                   # Implement code gọi Insert/Update/Delete DB
│
└── 4. WebApi/                          # Tầng Nhận Request
    ├── Controllers/                    # (Auth, User, Project, Report Controller)
    ├── Middlewares/                    # Exception Middleware hứng lỗi chung
    └── Program.cs                      # Nơi đăng ký DI Container và Middleware
```
