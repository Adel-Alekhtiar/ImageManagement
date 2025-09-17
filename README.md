# ImageManagment Solution
## Summary
this project is a simple image managment ASP.NET Core Web API application. 
It allows users to upload, retrive and delete images as base64encoded string The API Dosn't convert or validate the image string, it just simply stores it for a customer or a lead.
## there are 2 projects in this soloution 
`DAL` The Data Access Layer, which implements separation of concerns and utilizes dependency injection.

`ImageManagementAPI` The RESTFUL API, which exposes three action methods for managing users’ images.

## there are 4 models

`ContactBase` which is a base class that `Lead` and `Customer` drive from and `Image` model, the model design implements **TPH**.

so both `Lead` and `customer` data are stored in the same table **ContactBase**

the `Image` table has many-to-one relationship with `ContactBase` Table 

`UploadImages` method takes a json object with 2 properties Id which is a lead or customer id and a list of images.

`DeleteImage` method take 1 parameter id (the id of the image to be deleted.

`GetContactImages` method takes 1 paramter  (customer or lead) id, and retrives all the images belonging to that entity.
## to run the application follow these simple steps:

clone the repo then in the package manager console excute these commands

```
Add-Migration InitialCreate
Update-Database
```

Run the application, it will laucn in the broswer and you will see `swagger` UI listing all three methods and how to use them.
`
