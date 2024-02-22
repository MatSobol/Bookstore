## Book store app

### What it does

app does CRUD operations. Like delete book, create book etc.

in order to delete, update or create book you need to be loged in 

### How to run

you can run app with visual studio:

but firslty you need to start mysql server and change data in Api/appsetings.json

![image](https://github.com/MatSobol/Bookstore/assets/139177376/54404946-72a6-4617-80c8-2e5849d3a37d)

you can run development and production

![image](https://github.com/MatSobol/Bookstore/assets/139177376/55713799-2d08-468d-8009-fe145e81cc94)

if it ends with P it is production. In order for it to work you need to give proper url in appsetings.json:

![image](https://github.com/MatSobol/Bookstore/assets/139177376/778380c6-1b7f-4de6-9724-0154a68add95)

(Not necessary)

If you wish for login with google, microsoft or facebook to work you need to edit file Api/Program.cs in all places like this:

![image](https://github.com/MatSobol/Bookstore/assets/139177376/4844eff7-7381-44fe-8937-c4eeee48dcee)

in order to create proper ClientID and secret follow this tutorials and as redirecr url give for example http://localhost:5093/loginGoogle

Google

![image](https://github.com/MatSobol/Bookstore/assets/139177376/55c2432c-96f5-496c-a9d0-146c8fe97960)

Microsoft

![image](https://github.com/MatSobol/Bookstore/assets/139177376/88c18691-ddfc-4d21-b6e5-c84868053f28)

Facebook

https://theonetechnologies.com/blog/post/how-to-get-facebook-application-id-and-secret-key

### Login

for authentication api uses jwt with roles

account for login:

email: adminemail@gmail.com

password: string

### How it looks

api can be run on public server
![image](https://github.com/MatSobol/Bookstore/assets/139177376/67a31bec-508c-4bb5-9100-0d8330649f95)

how blazor app looks like

![image](https://github.com/MatSobol/Bookstore/assets/139177376/81ef98e0-3b25-4765-8c5e-c3beb874dd3f)


how it looks on mobile

![image](https://github.com/MatSobol/Bookstore/assets/139177376/0876920e-c7f2-48f8-8668-0e78b96e7772)

login on mobile

![image](https://github.com/MatSobol/Bookstore/assets/139177376/d61c739a-47d9-4b90-bb15-05720f57db14)


