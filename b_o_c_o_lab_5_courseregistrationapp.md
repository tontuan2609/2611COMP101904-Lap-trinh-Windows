# BÀI LAB BUỔI 5 - WINDOWS FORMS CƠ BẢN
**Học phần:** COMP1019 - Lập trình trên Windows  
**Giảng viên:** ThS. Lê Thanh Thoại - Khoa Công nghệ Thông tin, HCMUE  

## 📌 Thông tin sinh viên
- **Họ và tên:** [Nhập họ tên của bạn vào đây]
- **Mã sinh viên:** [Nhập MSSV vào đây]
- **Lớp:** [Nhập lớp của bạn vào đây]

## 1. Giới thiệu dự án
**CourseRegistrationApp** là một ứng dụng Windows Forms cơ bản được xây dựng bằng ngôn ngữ C#. Ứng dụng mô phỏng hệ thống đăng ký khóa học cho học viên, cho phép người dùng nhập thông tin cá nhân, chọn khóa học, hình thức học, số tháng và tự động tính toán tổng học phí.

Dự án này là kết quả của bài thực hành Lab 5 nhằm mục tiêu nắm vững cách sử dụng Form Designer, Toolbox, Properties và các Event (Click, Load, SelectedIndexChanged, ValueChanged) trong Windows Forms.

## 🚀 Các tính năng chính
- **Khởi tạo dữ liệu (Form Load):** Tự động tải danh sách các khóa học (C# WinForms, SQL Server, Web Frontend, Python), thiết lập mặc định hình thức học Online và số tháng là 1.
- **Tính toán học phí động:** Tổng học phí được cập nhật tự động ngay khi người dùng thay đổi khóa học hoặc số tháng đăng ký.
- **Đăng ký:**
  - Kiểm tra tính hợp lệ của dữ liệu (Họ tên và Số điện thoại không được để trống).
  - Hiển thị bảng tóm tắt thông tin đăng ký thông qua `MessageBox`.
- **Làm mới (Reset):** Xóa toàn bộ dữ liệu đã nhập, đặt các control về trạng thái mặc định ban đầu và đưa con trỏ chuột về ô Họ tên.
- **Thoát an toàn:** Hiển thị hộp thoại xác nhận trước khi đóng ứng dụng để tránh thoát nhầm.

## 💻 Công nghệ sử dụng
- **Ngôn ngữ:** C#
- **Framework:** .NET Framework / Windows Forms
- **IDE:** Visual Studio

## 📸 Hình ảnh báo cáo giao diện
*(Hướng dẫn: Bạn hãy chụp ảnh màn hình ứng dụng lúc đang chạy và thay thế đường dẫn trong các thẻ bên dưới nhé)*

### 1. Giao diện chính của ứng dụng
![Giao diện chính](link_anh_giao_dien_vao_day.png)
*(Giao diện đầy đủ các GroupBox, TextBox, ComboBox, RadioButton...)*

### 2. Thông báo khi nhập thiếu dữ liệu
![Lỗi thiếu dữ liệu](link_anh_bao_loi_vao_day.png)
*(Demo chức năng bắt lỗi khi bấm Đăng ký mà để trống Họ tên/SĐT)*

### 3. Thông báo đăng ký thành công
![Đăng ký thành công](link_anh_thong_bao_thanh_cong.png)
*(Hiển thị MessageBox xác nhận thông tin đăng ký và tổng học phí)*

### 4. Hộp thoại xác nhận thoát
![Xác nhận thoát](link_anh_xac_nhan_thoat.png)

## 🎯 Cấu trúc control chính
Để code dễ đọc và quản lý, các control đã được đặt tên theo đúng quy chuẩn (Naming Convention), ví dụ:
- `txtHoTen`, `txtSoDienThoai` (TextBox)
- `cboKhoaHoc` (ComboBox)
- `radOnline`, `radOffline` (RadioButton)
- `numSoThang` (NumericUpDown)
- `btnDangKy`, `btnLamMoi`, `btnThoat` (Button)