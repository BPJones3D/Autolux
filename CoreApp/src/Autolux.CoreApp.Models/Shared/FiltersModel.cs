using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Autolux.CoreApp.Models.Shared;

public record FiltersModel
{
    public string filter = "Relevancy";
    public string filterOrder = "Descending";
    public string search_value = "";
    public int year_min = 0;
    public int year_max = 10000000;
    public float price_min = 0;
    public float price_max = 10000000;
    public int miles_min = 0;
    public int miles_max = 10000000;
    public int mpg_min = 0;
    public int mpg_max = 10000000;
    public float tankCapacity_min = 0;
    public float tankCapacity_max = 10000000;
    public int evRange_min = 0;
    public int evRange_max = 10000000;
    public int seatCount_min = 0;
    public int seatCount_max = 10000000;
    public int doorCount_min = 0;
    public int doorCount_max = 10000000;
    //public string fuelType = "";
    //public string transmission = "";
    //public string brand = "";
    public List<string> fuelType = [];
    public List<string> transmission = [];
    public List<string> brand = [];
}