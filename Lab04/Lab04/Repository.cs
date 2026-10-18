namespace Lab04
{
    public class Repository<T> where T : IEntity
    {
        private List<T> danhSach;

        public Repository()
        {
            danhSach = new List<T>();
        }

        public void Add(T phanTu)
        {
            danhSach.Add(phanTu);
        }
        public List<T> GetAll()
        {
            return danhSach.ToList();
        }
        public T? FindById(string id)
        {
            return danhSach.FirstOrDefault(x => x.Id == id);
        }
        public List<T> Find(Func<T, bool> thuocTinh)
        {
            return danhSach.Where(thuocTinh).ToList();
        }
        public void Remove(T phanTu)
        {
            danhSach.Remove(phanTu);
        }
    }
}