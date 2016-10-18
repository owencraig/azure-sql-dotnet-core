# Usage
* Create an app.config.json in the root of the project with
``` json
{
  "ConnectionStrings": {
    "EFContext": "<your connection string>"
  },
  "xml": {
    "ProductFile": "Product.xml",
    "CategoryFile": "Category.xml"
  }
}
```
* exec 
  * ```dotnet restore```
  * ```dotnet run```


# Outputs
Your db will have the tables created and populated within your azure db
