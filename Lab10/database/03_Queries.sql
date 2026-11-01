USE EduManagerDB;
GO

-- 1. Liệt kê toàn bộ sinh viên
SELECT MaSV, HoTen, NgaySinh, GioiTinh, MaLop FROM SINHVIEN;

-- 2. Liệt kê sinh viên kèm tên lớp và tên khoa
SELECT sv.MaSV, sv.HoTen, l.TenLop, k.TenKhoa
FROM SINHVIEN sv
INNER JOIN LOP l ON sv.MaLop = l.MaLop
INNER JOIN KHOA k ON l.MaKhoa = k.MaKhoa;

-- 3. Tìm sinh viên theo một phần họ tên bằng LIKE
SELECT MaSV, HoTen, MaLop FROM SINHVIEN WHERE HoTen LIKE N'%An%' ORDER BY HoTen ASC;

-- 4. Liệt kê sinh viên theo mã lớp
SELECT MaSV, HoTen, GioiTinh FROM SINHVIEN WHERE MaLop = 'CNTT01';

-- 5. Cập nhật họ tên của một sinh viên
UPDATE SINHVIEN SET HoTen = N'Nguyễn Văn An Updated' WHERE MaSV = 'SV001';

-- 6. Xóa một sinh viên chưa có dữ liệu điểm
DELETE FROM SINHVIEN WHERE MaSV = 'SV003' AND NOT EXISTS (SELECT 1 FROM KETQUA WHERE MaSV = 'SV003');

-- 7. Liệt kê điểm của từng sinh viên theo từng môn
SELECT sv.MaSV, sv.HoTen, mh.TenMH, kq.Diem
FROM KETQUA kq
INNER JOIN SINHVIEN sv ON kq.MaSV = sv.MaSV
INNER JOIN MONHOC mh ON kq.MaMH = mh.MaMH
ORDER BY sv.MaSV, mh.TenMH;

-- 8. Tính điểm trung bình của từng sinh viên
SELECT sv.MaSV, sv.HoTen, AVG(kq.Diem) AS DiemTrungBinh
FROM SINHVIEN sv
INNER JOIN KETQUA kq ON sv.MaSV = kq.MaSV
GROUP BY sv.MaSV, sv.HoTen;

-- 9. Thống kê số sinh viên theo từng lớp
SELECT l.MaLop, l.TenLop, COUNT(sv.MaSV) AS SoSinhVien
FROM LOP l
LEFT JOIN SINHVIEN sv ON l.MaLop = sv.MaLop
GROUP BY l.MaLop, l.TenLop;

-- 10. Liệt kê các lớp có từ 2 sinh viên trở lên
SELECT l.MaLop, l.TenLop, COUNT(sv.MaSV) AS SoSinhVien
FROM LOP l
LEFT JOIN SINHVIEN sv ON l.MaLop = sv.MaLop
GROUP BY l.MaLop, l.TenLop
HAVING COUNT(sv.MaSV) >= 2;

-- 11. Test lỗi khi thêm sinh viên vào lớp không tồn tại (Sẽ báo lỗi khóa ngoại)
INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, MaLop)
VALUES ('SV999', N'Sinh viên lỗi', '2005-01-01', N'Nam', 'CNTT99');

-- 12. Test lỗi khi nhập điểm ngoài khoảng 0 đến 10 (Sẽ báo lỗi CHECK constraint)
INSERT INTO KETQUA (MaSV, MaMH, Diem)
VALUES ('SV001', 'COMP1019', 12);