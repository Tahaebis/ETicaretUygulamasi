using ETicaretUygulamasi.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ETicaretUygulamasi.Business
{
    public interface IAddressManager
    {
        void Add(Address model);
        bool AddressExists(int id);
        Address Find(int id);
        Address Find(int id, bool withAsNoTracking);
        bool IsNullAddresses();
        List<Address> List(int userId);
        void Remove(Address address);
        void Update(Address model);
    }

    public class AddressManager : IAddressManager
    {
        private readonly DatabaseContext _db;

        public AddressManager(DatabaseContext db)
        {
            _db = db;
        }

        public List<Address> List(int userId)
        {
            List<Address> addresses = _db.Addresses.Where(x => x.UserId == userId).ToList();
            return addresses;
        }

        public bool IsNullAddresses()
        {
            //bool result = false;

            //if (_db.Addresses == null)
            //{
            //    result = true;
            //}

            //return result;

            return _db.Addresses == null;
        }

        public bool AddressExists(int id)
        {
            return (_db.Addresses?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public Address Find(int id)
        {
            return _db.Addresses
                .Include(a => a.User)
                .FirstOrDefault(m => m.Id == id);
        }

        public Address Find(int id, bool withAsNoTracking)
        {
            if (withAsNoTracking)
            {
                return _db.Addresses.AsNoTracking().FirstOrDefault(x => x.Id == id);
            }
            else
            {
                return Find(id);
            }
        }

        public void Add(Address model)
        {
            _db.Add(model);
            _db.SaveChanges();
        }

        public void Update(Address model)
        {
            _db.Update(model);
            _db.SaveChanges();
        }

        public void Remove(Address address)
        {
            _db.Addresses.Remove(address);
            _db.SaveChanges();
        }
    }

    public class AddressManager2 : IAddressManager
    {
        public void Add(Address model)
        {
            throw new NotImplementedException();
        }

        public bool AddressExists(int id)
        {
            throw new NotImplementedException();
        }

        public Address Find(int id)
        {
            throw new NotImplementedException();
        }

        public Address Find(int id, bool withAsNoTracking)
        {
            throw new NotImplementedException();
        }

        public bool IsNullAddresses()
        {
            throw new NotImplementedException();
        }

        public List<Address> List(int userId)
        {
            throw new NotImplementedException();
        }

        public void Remove(Address address)
        {
            throw new NotImplementedException();
        }

        public void Update(Address model)
        {
            throw new NotImplementedException();
        }
    }
}
