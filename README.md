# PaymentSystemAPI

## Description
Welcome to Payment System API! This is an API that can be called to create merchants and customers; read merchant and customer details; update merchant and customer records. You can also use the API to delete merchant and customer records when the need arises. The API was made using .NET 6 framework and a few tools. I hope you will enjoy the API as you use it to manage your merchants and customer records. I also look forward to your contributions!

## Installation / Usage
1. Clone the source code from this repo link: https://github.com/aduramomi/PaymentSystemApi
2. Go to the connection string section in the appsettings.json file and change the port no on which PostgreSQL Database is listening on in case if that port no is in use by another app on your dev/prod env
3. Lauch the project in debug mode, and browse to this link https://localhost:7161/swagger/index.html to see the Swagger documentations of the end points

## Merchant EndPoints
1. GetAll();
2. GetMerchantByMerchantNumber()
3. GetMerchantById()
4. GetMerchant()
5. CreateMerchant()
6. UpdateMerchant
7. DeleteMerchant
 
## Customer EndPoints
1. GetAll()
2. GetCustomerByCustomerNumber()
3. GetCustomerById()
4. GetCustomer()
5. CreateCustomer()
6. UpdateCustomer
7. DeleteCustomer
