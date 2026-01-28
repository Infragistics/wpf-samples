using System;
using System.Collections.ObjectModel;

namespace Infragistics.Samples.Shared.Models
{
   
    /// <summary>
    /// Represents a collection of Company objects with a sample of 5 radomly generated items
    /// </summary>
    public class CompanyDataSample : ObservableCollection<Company>
    {
        public CompanyDataSample()
        {
            ObservableCollection<Company> companies = CompanyDataGenerator.GenerateCompanies(5, 10, 4);
            foreach (Company company in companies)
            {
                this.Add(company);
            }
        }
    }
    public class CompanyDataGenerator
    {
        private static readonly Random Random = new Random();

        public static ObservableCollection<Company> GenerateCompanies(int companyCount, int productPerCompanyCount, int salesPerProductCount)
        {
            ObservableCollection<Company> companies = new ObservableCollection<Company>();
            for (double i = 0; i < companyCount; i++)
            {
                Company company = new Company();
                company.Name = "C" + i;
                ObservableCollection<Product> products = ProductDataGenerator.GenerateProducts(productPerCompanyCount, salesPerProductCount);
                foreach (Product product in products)
                {
                    company.Products.Add(product);
                    company.Revenue += product.Revenue;
                }
 
                companies.Add(company);
            }
            return companies;
        }
    }
}