
# Diary.Api

Create an online diary, organizer, and contacts manager. Using a calendar-based interface it allows
you to add, delete, and edit a diary entry for any day. 

## Technologies

* .Net Core 6 Web Api 
* .Net Core 6  Class Library



## Architecture 

* Onion Architecture
## Patterns

* Generic Repository Pattern
* Unit of Work pattern
* Service locator pattern 
* Options pattern
* Rate Limiting pattern
* HATEOAS 
* Caching 
## Features

- Register/login for user
- Diary
    *  Diary Contains details of all  Diary users, their DiaryId, and names.
    *  Diary do most of the work of holding diary data , retrieving and storing
- DiaryEntry
    * The DiaryEntry class objectifies a single entry in a diary. It encapsulates everything to do with diary
    * entries, including creating, updating, and retrieving diary entry data.It handles all the database access for diary entries.
- Contact
    * The Contact class objectifies a single contact — a person or thing for which you want to store contact
    * information.It encapsulates everything to do with contacts, including the storing and retrieving of contact information in the database.
- DiaryEvent
    * The DiaryEvent class objectifies a single entry in a diary. It encapsulates everything to do with diary
    * entries, including creating, updating, and retrieving diary events data.It handles all the database access for diary events.


## Documentation

[Documentation](https://documenter.getpostman.com/view/15522322/2s8Z6vYE87)



##  Run project

Change connection string 

```bash
  dotnet ef database update
```

```bash
   dotnet run --project Api.Diary
```


## 🚀 About Me
I'm a .Net Backend developer with 2 years of experience in software development .


## 🔗 Links
[![linkedin](https://img.shields.io/badge/linkedin-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/khaled-soltan/)
[![twitter](https://img.shields.io/badge/twitter-1DA1F2?style=for-the-badge&logo=twitter&logoColor=white)](https://twitter.com/KhalSoltan/)

