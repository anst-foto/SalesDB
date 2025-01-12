using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using Microsoft.Extensions.Configuration;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SalesDB.BL;
using SalesDB.Models;

namespace SalesDB.Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly SalesServices _salesServices;

    public ObservableCollection<Sales> Sales { get; } = [];

    [Reactive] public string ProductName { get; set; }
    [Reactive] public decimal ProductPrice { get; set; }

    [Reactive] public int SaleProductId { get; set; }
    [Reactive] public int SaleAmount { get; set; }
    [Reactive] public DateTime SaleDate { get; set; }

    [Reactive] public bool IsFormAddProduct { get; set; }
    [Reactive] public bool IsFormAddSale { get; set; }

    public ReactiveCommand<Unit, Unit> CommandRefresh { get; }
    public ReactiveCommand<Unit, Unit> CommandAddProduct { get; }
    public ReactiveCommand<Unit, Unit> CommandAddSale { get; }
    public ReactiveCommand<string, Unit> CommandSave { get; }

    public MainWindowViewModel()
    {
        var connectionString = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build()
            #if DEBUG
            .GetConnectionString("ConnectionToTestDB");
            #elif RELEASE
            .GetConnectionString("ConnectionToPublicDB");
            #endif

        _salesServices = new SalesServices(connectionString);

        Refresh();
        IsFormAddProduct = false;
        IsFormAddSale = false;

        CommandRefresh = ReactiveCommand.Create(Refresh);

        CommandAddSale = ReactiveCommand.Create((() =>
        {
            IsFormAddSale = true;
            IsFormAddProduct = false;
        }));

        CommandAddProduct = ReactiveCommand.Create((() =>
        {
            IsFormAddSale = false;
            IsFormAddProduct = true;
        }));

        CommandSave = ReactiveCommand.Create<string, Unit>(Save);
    }

    private void Refresh()
    {
        Sales.Clear();

        var sales = _salesServices.GetAllSales();
        foreach (var sale in sales)
        {
            Sales.Add(sale);
        }
    }

    private Unit Save(string name)
    {
        switch (name)
        {
            case "AddProduct":
                _salesServices.AddProduct(new Product()
                {
                    Name = ProductName,
                    Price = ProductPrice
                });
                Refresh();
                break;

            case "AddSale":
                _salesServices.AddSale(new Sale()
                {
                    ProductId = SaleProductId,
                    Amount = SaleAmount,
                    Date = SaleDate
                });
                Refresh();
                break;
        }
        return new Unit();
    }
}
