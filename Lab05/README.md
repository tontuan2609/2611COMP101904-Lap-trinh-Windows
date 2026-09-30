# 2611COMP101904 - Lập trình Windows - Lab05
## MSSV: 47.01.104.229
## Họ và tên: Tôn Thất Tuấn
## Lớp: 49.01.CNTT.B
## Nhóm: 11
## 1. Mô tả bài tập
Dự án `CourseRegistrationApp` là một ứng dụng WinForms được xây dựng bằng C# nhằm mô phỏng nghiệp vụ đăng ký khóa học. Ứng dụng tập trung vào việc thiết kế giao diện bằng Form Designer và xử lý dữ liệu trực tiếp trên Form, chưa kết nối với cơ sở dữ liệu. 
**Các chức năng và yêu cầu kỹ thuật chính:**
- **Thiết kế giao diện:** Bố cục chia thành các nhóm thông tin rõ ràng bằng `GroupBox`, sử dụng đa dạng các Control cơ bản (TextBox, ComboBox, RadioButton, DateTimePicker...) và chuẩn hóa việc đặt tên Control, thứ tự Tab Order.
- **Khởi tạo dữ liệu:** Tự động nạp danh sách khóa học và các thông số mặc định (hình thức Online, số tháng đăng ký là 1) ngay khi Form Load.
- **Xử lý tính toán:** Tự động tính và hiển thị tổng học phí (bằng học phí một tháng nhân với số tháng) mỗi khi có sự thay đổi về lựa chọn khóa học hoặc thời gian học.
- **Kiểm duyệt dữ liệu:** Nút Đăng ký yêu cầu kiểm tra tính hợp lệ của dữ liệu (Họ tên và Số điện thoại không được để trống, phải chọn khóa học) trước khi thực thi.
- **Xuất kết quả:** Hiển thị phiếu đăng ký tổng hợp đầy đủ thông tin học viên và khóa học thông qua hộp thoại `MessageBox`.
- **Tiện ích bổ sung:** Nút Làm mới giúp dọn dẹp Form về trạng thái ban đầu, và nút Thoát tích hợp hộp thoại xác nhận an toàn.
## 2. Kết quả đạt được
Dự án đã hoàn thiện toàn bộ các tiêu chí đánh giá của bài Lab:
- Khởi tạo thành công project và giao diện hoạt động ổn định.
- Logic tính toán học phí phản hồi chính xác theo thời gian thực.
- Các chức năng kiểm tra lỗi nhập liệu và hiển thị kết quả phiếu đăng ký hoạt động đúng thiết kế.
### 2.1. Trạng thái khởi tạo
Giao diện khi vừa mở ứng dụng, dữ liệu khóa học và các thiết lập mặc định đã được nạp sẵn:
![Giao diện khởi tạo](form_load.png"Giao diện khởi tạo")
### 2.2. Kết quả xử lý đăng ký
Giao diện hiển thị phiếu đăng ký sau khi người dùng điền đầy đủ thông tin hợp lệ và phần mềm tính toán tổng tiền thành công:
![Giao diện kết quả](form_result.png"Giao diện kết quả")