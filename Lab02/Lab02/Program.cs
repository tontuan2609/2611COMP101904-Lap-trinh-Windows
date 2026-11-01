namespace Lab02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] a = new int[0];
            int choice;

            do
            {
                Console.WriteLine("\n===== MENU =====");
                Console.WriteLine("1. Nhap mang");
                Console.WriteLine("2. Xuat mang");
                Console.WriteLine("3. Tinh tong");
                Console.WriteLine("4. Tim max/min");
                Console.WriteLine("5. Dem chan/le");
                Console.WriteLine("6. Sap xep tang dan");
                Console.WriteLine("7. Tim kiem");
                Console.WriteLine("0. Thoat");

                choice = NhapSoNguyen("Chon chuc nang: ");

                if (choice >= 2 && choice <= 7 && a.Length == 0)
                {
                    Console.WriteLine("=> Canh bao: Ban chua nhap mang! Vui long chon chuc nang 1 truoc.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        a = NhapMang();
                        break;
                    case 2:
                        Console.WriteLine("=> Cac phan tu trong mang la:");
                        XuatMang(a);
                        break;
                    case 3:
                        Console.WriteLine($"=> Tong cac phan tu trong mang: {TinhTong(a)}");
                        break;
                    case 4:
                        Console.WriteLine($"=> Gia tri lon nhat (Max): {TimMax(a)}");
                        Console.WriteLine($"=> Gia tri nho nhat (Min): {TimMin(a)}");
                        break;
                    case 5:
                        Console.WriteLine($"=> So luong phan tu chan: {DemChan(a)}");
                        Console.WriteLine($"=> So luong phan tu le: {DemLe(a)}");
                        break;
                    case 6:
                        SapXepTangDan(a);
                        Console.WriteLine("=> Mang sau khi sap xep tang dan:");
                        XuatMang(a);
                        break;
                    case 7:
                        int x = NhapSoNguyen("Nhap gia tri can tim x = ");
                        int index = TimKiem(a, x);
                        if (index != -1)
                        {
                            Console.WriteLine($"=> Tim thay {x} trong mang tai vi tri dau tien la {index} (tinh tu 0).");
                        }
                        else
                        {
                            Console.WriteLine($"=> Khong tim thay {x} trong mang.");
                        }
                        break;
                    case 0:
                        Console.WriteLine("=> Chuong trinh ket thuc. Tam biet!");
                        break;
                    default:
                        Console.WriteLine("=> Loi: Lua chon khong hop le. Vui long chon tu 0 den 7!");
                        break;
                }

            } while (choice != 0);
        }

        static int NhapSoNguyen(string message)
        {
            int result;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out result))
                {
                    return result;
                }
                Console.WriteLine("Loi: Gia tri nhap vao khong hop le. Vui long nhap mot so nguyen!");
            }
        }

        static int NhapSoNguyenDuong(string message)
        {
            int result;
            while (true)
            {
                result = NhapSoNguyen(message);
                if (result > 0)
                {
                    return result;
                }
                Console.WriteLine("Loi: Gia tri phai la so nguyen duong (n > 0). Vui long nhap lai!");
            }
        }

        static int[] NhapMang()
        {
            int n = NhapSoNguyenDuong("Nhap so luong phan tu n = ");
            int[] a = new int[n];
            for (int i = 0; i < n; i++)
            {
                a[i] = NhapSoNguyen($"Nhap phan tu a[{i}] = ");
            }
            return a;
        }

        static void XuatMang(int[] a)
        {
            Console.WriteLine(string.Join(" ", a));
        }

        static int TinhTong(int[] a)
        {
            int sum = 0;
            foreach (int item in a)
            {
                sum += item;
            }
            return sum;
        }

        static int TimMax(int[] a)
        {
            int max = a[0];
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] > max) max = a[i];
            }
            return max;
        }

        static int TimMin(int[] a)
        {
            int min = a[0];
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] < min) min = a[i];
            }
            return min;
        }

        static int DemChan(int[] a)
        {
            int count = 0;
            foreach (int item in a)
            {
                if (item % 2 == 0) count++;
            }
            return count;
        }

        static int DemLe(int[] a)
        {
            int count = 0;
            foreach (int item in a)
            {
                if (item % 2 != 0) count++;
            }
            return count;
        }

        static void SapXepTangDan(int[] a)
        {
            Array.Sort(a);
        }

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