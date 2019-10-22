using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WebShopService.App_Data;

namespace WebShopService
{
    public class HappyTailsService : IHappyTailsService
    {
        private HappyTailsEntities db = new HappyTailsEntities();


        public List<ProductInformation> GetAllProducts()
        {
            //Balance property should be used to check if there is available products -- but is not used for now.
            //ImagePath should be used in html code for showing the product picture -- is not used for now either and doesn't show.

            using (db)
            {
                var products = from prod in db.Product
                               select prod;

                List<ProductInformation> allProducts = new List<ProductInformation>();

                foreach (var row in products)
                {
                    allProducts.Add(new ProductInformation
                    {
                        Id = row.ProductID, Name = row.Name, Brand = row.Brand,
                        Color = row.Color, Description = row.Description, Price = row.Price, AnimalCategory = row.AnimalCategory,
                        Height = row.Height, Length = row.Length, Balance = row.Balance
                    });
                }

                return allProducts;
            }
        }

        public void AddProduct(ProductInformation newProduct)
        {
            using (db)
            {
                //Could have a List<ProductInformation> instead - to add more than one decimal property at a time
                db.Product.Add(new Product
                {
                    Name = newProduct.Name, Price = newProduct.Price, Brand = newProduct.Brand,
                    Color = newProduct.Color, Description = newProduct.Description, Height = newProduct.Height,
                    ImagePath = newProduct.ImagePath, Length = newProduct.Length, Balance = newProduct.Balance,
                    AnimalCategory = newProduct.AnimalCategory
                });
                db.SaveChanges();
            }
        }

        public void EditProduct(ProductInformation product)
        {
            using (db)
            {
                //Fetching the products id, to be able to edit the right product
                Product editedProduct = db.Product.Find(product.Id);

                //Then the new values of the properties are stored in the new variable
                if (product.Name != null)
                {
                    editedProduct.Name = product.Name;
                }

                if (product.Balance != null)
                {
                    editedProduct.Balance = product.Balance;
                }

                if (product.Brand != null)
                {
                    editedProduct.Brand = product.Brand;
                }

                if (product.Color != null)
                {
                    editedProduct.Color = product.Color;
                }

                if (product.Description != null)
                {
                    editedProduct.Description = product.Description;
                }

                if (product.Height != null)
                {
                    editedProduct.Height = product.Height;
                }

                if (product.ImagePath != null)
                {
                    editedProduct.ImagePath = product.ImagePath;
                }

                if (product.Length != null)
                {
                    editedProduct.Length = product.Length;
                }

                if (product.Price != null)
                {
                    editedProduct.Price = product.Price;
                }

                if (product.AnimalCategory != null)
                {
                    editedProduct.AnimalCategory = product.AnimalCategory;
                }

                db.Entry(editedProduct).State = EntityState.Modified;
                db.SaveChanges();
            }    
        }

        public void DeleteProduct(int id)
        {
            using (db)
            {
                db.Product.Remove(db.Product.Find(id) ?? throw new InvalidOperationException());
                db.SaveChanges();
            }
        }

        public List<CustomerInformation> GetAllCustomers()
        {
            //The customers password is retrieved for now - but it could (and maybe should) be alter so that method doesn't 
            using (db)
            {
                var customers = from cust in db.Customer
                    select cust;

                List<CustomerInformation> allCustomers = new List<CustomerInformation>();

                foreach (var row in customers)
                {
                    allCustomers.Add(new CustomerInformation
                    {
                        Id = row.CustomerID, Firstname = row.Firstname, Surname = row.Surname, Email = row.Email,
                        Username = row.Username, Street = row.Street, ZipCode = row.ZipCode, Password = row.Password
                    });
                }

                return allCustomers;
            }
        }

        //In the future the method should be done that customers cannot
        //be added if admins fills out a email and/or a username that already exists in the database. 
        public void AddCustomer(CustomerInformation newCustomer)
        {
            using (db)
            {
                db.Customer.Add(new Customer
                {
                    Firstname = newCustomer.Firstname, Surname = newCustomer.Surname,
                    Password = newCustomer.Password, Username = newCustomer.Username,
                    Email = newCustomer.Email, Street = newCustomer.Street, ZipCode = newCustomer.ZipCode
                });
                db.SaveChanges();
            }
        }

        //Admins should be able to edit most properties of customers
        //This if a customer should mail or call the business (HappyTails) and want help to change some information about them
        public void EditCustomer(CustomerInformation customer)
        {
            using (db)
            {
                Customer editedCustomer = db.Customer.Find(customer.Id);

                //The username should not be able for admins to alter

                if (customer.Firstname != null)
                {
                    editedCustomer.Firstname = customer.Firstname;
                }

                if (customer.Password != null)
                {
                    editedCustomer.Password = customer.Password;
                }

                if (customer.Surname != null)
                {
                    editedCustomer.Surname = customer.Surname;
                }

                if (customer.Email != null)
                {
                    editedCustomer.Email = customer.Email;
                }

                if (customer.Street != null)
                {
                    editedCustomer.Street = customer.Street;
                }

                if (customer.ZipCode != null)
                {
                    editedCustomer.ZipCode = customer.ZipCode;
                }
                
                db.Entry(editedCustomer).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteCustomer(int id)
        {
            using (db)
            {
                db.Customer.Remove(db.Customer.Find(id) ?? throw new InvalidOperationException());
                db.SaveChanges();
            }
        }

        //This method is not used at all for now.
        public void AddPurchase(PurchaseInformation newPurchase)
        {
            //Method for TotalPrice should be outside this method -- but is not used for now.
            using (db)
            {
                db.Purchase.Add(new Purchase
                {
                    CustomerID = newPurchase.CustomerId,
                    NumberOfProducts = newPurchase.NumberOfProducts,
                    PurchaseDate = newPurchase.PurchaseDate,
                    TotalPrice = newPurchase.TotalPrice
                });
                db.SaveChanges();
            }
        }

        public List<PurchaseInformation> GetPurchasesFromCustomer(int id)
        {
            using (db)
            {
                //Fetches the customer-id from purchase table and that it matches the one in the customer table
                //and thereafter that the customer-id matches the current customer (id that is passed as a value) -->
                //get all his/hers purchases

                var purchases = from p in db.Purchase
                                join c in db.Customer on p.CustomerID equals c.CustomerID
                                where c.CustomerID == id
                                select p;

                List<PurchaseInformation> allCustomerPurchases = new List<PurchaseInformation>();

                foreach (var row in purchases)
                {
                    allCustomerPurchases.Add(new PurchaseInformation
                    {
                        Id = row.PurchaseID, PurchaseDate = row.PurchaseDate, NumberOfProducts = row.NumberOfProducts,
                        TotalPrice = row.TotalPrice
                        //The method for GetTotalPrice() should be here -- when it is developed.
                    });
                }

                return allCustomerPurchases;
            }
        }

        //Will be worked on in further development.
        public decimal GetTotalPrice(int id)
        {
            var sum = 0;
            //Will need to check which products the customer has in his/hers shopping cart and/or his/hers order (purchase)
            //Then it is needed to check how many of the specific product the customer has picked
            //and the price for every specific product will need to be fetched
            //This will be added to the sum which will be returned

            return sum;
        }
    }
}
