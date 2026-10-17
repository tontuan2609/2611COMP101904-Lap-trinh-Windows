namespace Lab02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            int[] a = new int[0];
            while (true)
            {
                Menu();
                int n = NhapSoNguyen("Chọn chức năng: ");
                if (a.Length == 0 && n >= 2 && n <= 7)
                {
                    Console.WriteLine("Phải nhập mảng!");
                    continue;
                }
                switch (n)
                {
                    case 1:
                        a = NhapMang();
                        break;
                    case 2:
                        XuatMang(a);
                        break;
                    case 3:
                        Console.WriteLine($"Tổng = {TinhTong(a)}");
                        break;
                    case 4:
                        Console.WriteLine($"Max = {TimMax(a)}");
                        Console.WriteLine($"Min = {TimMin(a)}");
                        break;
                    case 5:
                        Console.WriteLine($"Chẵn = {DemChan(a)}");
                        Console.WriteLine($"Lẻ = {DemLe(a)}");
                        break;
                    case 6:
                        SapXepTangDan(a);
                        XuatMang(a);
                        break;
                    case 7:
                        int x = NhapSoNguyen("x = ");
                        if (TimKiem(a, x) == -1)
                        {
                            Console.WriteLine($"Không tìm thấy x = {x}");
                        }
                        else
                        {
                            Console.WriteLine($"Tìm thấy x = {x} tại vị trí i = {TimKiem(a, x)}");
                        }
                        break;
                    case 0:
                        Console.WriteLine("Thoát");
                        return;
                    default:
                        Console.WriteLine("Phải nhập từ 0 đến 7!");
                        break;
                }
            }
        }

        // Menu
        static void Menu()
        {
            Console.WriteLine();
            Console.WriteLine("===== MENU =====");
            Console.WriteLine("1. Nhập mảng");
            Console.WriteLine("2. Xuất mảng");
            Console.WriteLine("3. Tính tổng");
            Console.WriteLine("4. Tìm max/ min");
            Console.WriteLine("5. Đếm chẵn/ lẻ");
            Console.WriteLine("6. Sắp xếp tăng dần");
            Console.WriteLine("7. Tìm kiếm");
            Console.WriteLine("0. Thoát");
        }

        // Nhập số nguyên
        static int NhapSoNguyen(string s)
        {
            while (true)
            {
                Console.Write(s);
                if (int.TryParse(Console.ReadLine(), out int n))
                {
                    return n;
                }
                Console.WriteLine("Phải nhập số nguyên!");
            }
        }

        // Nhập số nguyên dương
        static int NhapSoNguyenDuong(string s)
        {
            while (true)
            {
                int n = NhapSoNguyen(s);
                if (n > 0)
                {
                    return n;
                }
                Console.WriteLine("Phải nhập số nguyên dương!");
            }
        }

        // Nhập mảng
        static int[] NhapMang()
        {
            int n = NhapSoNguyenDuong("n = ");
            int[] a = new int[n];
            for (int i = 0; i < n; i++)
            {
                a[i] = NhapSoNguyen($"a[{i}] = ");
            }
            return a;
        }

        // Xuất mảng
        static void XuatMang(int[] a)
        {
            Console.Write("Xuất mảng: ");
            for (int i = 0; i < a.Length; i++)
            {
                Console.Write($"a[{i}] = {a[i]}, ");
            }
            Console.WriteLine();
        }

        // Tính tổng
        static int TinhTong(int[] a)
        {
            int x = 0;
            for (int i = 0; i < a.Length; i++)
            {
                x = x + a[i];
            }
            return x;
        }

        // Tìm max
        static int TimMax(int[] a)
        {
            int max = a[0];
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] > max)
                {
                    max = a[i];
                }
            }
            return max;
        }

        // Tìm min
        static int TimMin(int[] a)
        {
            int min = a[0];
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] < min)
                {
                    min = a[i];
                }
            }
            return min;
        }

        // Đếm chẵn
        static int DemChan(int[] a)
        {
            int x = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] % 2 == 0)
                {
                    x++;
                }
            }
            return x;
        }

        // Đếm lẻ
        static int DemLe(int[] a)
        {
            int x = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] % 2 != 0)
                {
                    x++;
                }
            }
            return x;
        }

        // Sắp xếp tăng dần
        static void SapXepTangDan(int[] a)
        {
            for (int i = 0; i < a.Length - 1; i++)
            {
                for (int j = i + 1; j < a.Length; j++)
                {
                    if (a[i] > a[j])
                    {
                        int x = a[i];
                        a[i] = a[j];
                        a[j] = x;
                    }
                }
            }
        }

        // Tìm kiếm
        static int TimKiem(int[] a, int x)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == x)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}