using IGDataGrid.Samples.Helpers;
using System.Collections.Generic;

namespace IGDataGrid.Samples.ViewModel
{
    public class PersonFoodsDataSource
    {
        private List<FavoriteFoodPerson> persons;
        private List<FavoriteFoodPersonLimited> personsLimited;

        public PersonFoodsDataSource()
        {
            int id = 0;
            persons = new List<FavoriteFoodPerson>();
            persons.Add(new FavoriteFoodPerson() { ID = id++, Name = "Maria Anders", Food = FoodsAll.NotSelected, Beverage = BeveragesAll.NotSelected });
            persons.Add(new FavoriteFoodPerson() { ID = id++, Name = "Ana Trujillo", Food = FoodsAll.NotSelected, Beverage = BeveragesAll.NotSelected });
            persons.Add(new FavoriteFoodPerson() { ID = id++, Name = "Thomas Hardy", Food = FoodsAll.NotSelected, Beverage = BeveragesAll.NotSelected });
            persons.Add(new FavoriteFoodPerson() { ID = id++, Name = "Laurence Lebihan", Food = FoodsAll.NotSelected, Beverage = BeveragesAll.NotSelected });
            persons.Add(new FavoriteFoodPerson() { ID = id++, Name = "Elizabeth Lincoln", Food = FoodsAll.NotSelected, Beverage = BeveragesAll.NotSelected });
            persons.Add(new FavoriteFoodPerson() { ID = id++, Name = "Francisco Chang", Food = FoodsAll.NotSelected, Beverage = BeveragesAll.NotSelected });
            persons.Add(new FavoriteFoodPerson() { ID = id++, Name = "Pedro Afonso", Food = FoodsAll.NotSelected, Beverage = BeveragesAll.NotSelected });

            id = 0;
            personsLimited = new List<FavoriteFoodPersonLimited>();
            personsLimited.Add(new FavoriteFoodPersonLimited() { ID = id++, Name = "Maria Anders", Food = FoodsLimited.NotSelected, Beverage = BeveragesLimited.NotSelected });
            personsLimited.Add(new FavoriteFoodPersonLimited() { ID = id++, Name = "Ana Trujillo", Food = FoodsLimited.NotSelected, Beverage = BeveragesLimited.NotSelected });
            personsLimited.Add(new FavoriteFoodPersonLimited() { ID = id++, Name = "Thomas Hardy", Food = FoodsLimited.NotSelected, Beverage = BeveragesLimited.NotSelected });
            personsLimited.Add(new FavoriteFoodPersonLimited() { ID = id++, Name = "Laurence Lebihan", Food = FoodsLimited.NotSelected, Beverage = BeveragesLimited.NotSelected });
            personsLimited.Add(new FavoriteFoodPersonLimited() { ID = id++, Name = "Elizabeth Lincoln", Food = FoodsLimited.NotSelected, Beverage = BeveragesLimited.NotSelected });
            personsLimited.Add(new FavoriteFoodPersonLimited() { ID = id++, Name = "Francisco Chang", Food = FoodsLimited.NotSelected, Beverage = BeveragesLimited.NotSelected });
            personsLimited.Add(new FavoriteFoodPersonLimited() { ID = id++, Name = "Pedro Afonso", Food = FoodsLimited.NotSelected, Beverage = BeveragesLimited.NotSelected });
        }

        public List<FavoriteFoodPerson> ListOfPersonsAndTheirFavoriteFoods
        {
            get { return persons; }
        }

        public List<FavoriteFoodPersonLimited> ListOfPersonsAndTheirFavoriteFoodsLimited
        {
            get { return personsLimited; }
        }
    }
}
