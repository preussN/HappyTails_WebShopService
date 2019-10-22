using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WebShopService
{
    [ServiceContract]
    public interface IHappyTailsService
    {
        #region Methods for products
        [OperationContract]
        List<ProductInformation> GetAllProducts();

        [OperationContract]
        void AddProduct(ProductInformation newProduct);

        [OperationContract]
        void EditProduct(ProductInformation product);

        [OperationContract]
        void DeleteProduct(int id);
        #endregion

        #region Methods for customers
        [OperationContract]
        List<CustomerInformation> GetAllCustomers();

        [OperationContract]
        void AddCustomer(CustomerInformation newCustomer);

        [OperationContract]
        void EditCustomer(CustomerInformation customer);

        [OperationContract]
        void DeleteCustomer(int id);
        #endregion

        #region Methods for purchases -- which is not implemented yet
        [OperationContract]
        void AddPurchase(PurchaseInformation newPurchase);

        [OperationContract]
        decimal GetTotalPrice(int id);

        [OperationContract]
        List<PurchaseInformation> GetPurchasesFromCustomer(int id);
        #endregion
    }

    #region CustomerInformation
    [DataContract]
    public class CustomerInformation
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Firstname { get; set; }

        [DataMember]
        public string Surname { get; set; }

        [DataMember]
        public string Email  { get; set; }

        [DataMember]
        public string Street { get; set; }

        [DataMember]
        public int? ZipCode { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string Username { get; set; }
    }
    #endregion

    #region ProductInformation
    [DataContract]
    public class ProductInformation
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public decimal? Price { get; set; }

        [DataMember]
        public string Brand  { get; set; }

        [DataMember]
        public decimal? Length { get; set; }

        [DataMember]
        public decimal? Height { get; set; }

        [DataMember]
        public int? Balance { get; set; }

        [DataMember]
        public string ImagePath { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string AnimalCategory { get; set; }
    }
    #endregion

    #region PurchaseInformation
    [DataContract]
    public class PurchaseInformation
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        public int? NumberOfProducts { get; set; }

        [DataMember]
        public DateTime? PurchaseDate { get; set; }

        [DataMember]
        public decimal? TotalPrice { get; set; }
    }
    #endregion
}
