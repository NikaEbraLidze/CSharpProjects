### ნაბიჯი 1: პროექტის მომზადება (Code First)

შექმენი ახალი **Console App** (ან Web API, თუ გირჩევნია) და დააინსტალირე შემდეგი პაკეტები:

- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`

### ნაბიჯი 2: მონაცემთა მოდელები (Entities)

შექმენი 4 კლასი. ყურადღება მიაქციე კავშირებს (Relationships).

1. **Instructor (ლექტორი)**

- `Id` (int)
- `FullName` (string)
- `Courses` (List<Course>) - _One-to-Many კავშირი კურსებთან_

2. **Course (კურსი)**

- `Id` (int)
- `Title` (string)
- `Price` (decimal)
- `InstructorId` (int) - _Foreign Key_
- `Instructor` (Instructor) - _Navigation Property_
- `Students` (List<Student>) - _Many-to-Many სტუდენტებთან_

3. **Student (სტუდენტი)**

- `Id` (int)
- `Name` (string)
- `Email` (string)
- `Courses` (List<Course>) - _Many-to-Many კურსებთან_

4. **Review (შეფასება)** - _დამატებითი გამოწვევისთვის_

- `Id` (int)
- `Content` (string)
- `Rating` (int, 1-5)
- `CourseId` (int)
- `Course` (Course)

### ნაბიჯი 3: DbContext და კონფიგურაცია

შექმენი `AppDbContext` კლასი.

- ააწყვე კავშირი ბაზასთან (`OnConfiguring`).
- **მნიშვნელოვანი:** ჩართე ლოგირება, რომ კონსოლში ხედავდე რას აკეთებს EF (რა SQL-ს უშვებს).

```csharp
optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

```

- გაუშვი მიგრაციები (`add-migration`, `update-database`).

---

### ნაბიჯი 4: მონაცემების შევსება (Seeding)

დაწერე მეთოდი `SeedData()`, რომელიც ბაზაში ჩაწერს:

- 2 ლექტორს.
- 5 კურსს (გაანაწილე ლექტორებზე).
- 10 სტუდენტს (სტუდენტები მიაბი სხვადასხვა კურსებს).
- რამდენიმე შეფასებას კურსებისთვის.

---

### ნაბიჯი 5: პრაქტიკული ამოცანები (Core Tasks)

დაწერე `Repository` კლასი ან მეთოდები `Program.cs`-ში შემდეგი ლოგიკით.

#### დავალება 1: "მახე" (IEnumerable vs IQueryable)

1. შექმენი მეთოდი `GetExpensiveCourses()`.
2. ჯერ გამოიყენე `.AsEnumerable()` და მერე გაფილტრე ფასის მიხედვით (`Price > 50`).
3. დააკვირდი კონსოლს: ნახე, რომ SQL მოთხოვნაში `WHERE` არ წერია.
4. **გამოსწორება:** გადააკეთე `IQueryable`-ზე, რომ ფილტრაცია ბაზაში მოხდეს.

#### დავალება 2: Eager Loading (Include/ThenInclude)

1. შექმენი მეთოდი `GetCoursesWithDetails()`.
2. წამოიღე კურსები, თან წამოაყოლე **ლექტორი** (`Include`).
3. ასევე წამოაყოლე **სტუდენტები** (`Include`).
4. **ბონუსი:** თუ დაამატე `Review` კლასი, წამოაყოლე შეფასებებიც.

#### დავალება 3: Optimization (Select & DTO)

1. შექმენი კლასი `CourseDto` (მხოლოდ `Title`, `InstructorName`, `StudentsCount`).
2. დაწერე მეთოდი `GetCourseDtos()`.
3. გამოიყენე `.Select()`, რათა პირდაპირ დააკონვერტირო მონაცემები DTO-ში.
4. გამოიყენე `.AsNoTracking()`.
5. შეადარე ამ მეთოდის SQL ქვერი წინა დავალების (Include) SQL ქვერის. (ეს უნდა იყოს ბევრად მოკლე).

#### დავალება 4: Analytics (GroupBy)

1. დაწერე მეთოდი `GetInstructorStats()`.
2. დააჯგუფე კურსები `InstructorId`-ის მიხედვით.
3. დააბრუნე ანონიმური ობიექტი ან DTO:

- `InstructorName` (აქ დაგჭირდება ჯგუფში ჩახედვა).
- `TotalCourses` (რამდენი კურსი აქვს).
- `AveragePrice` (რა არის მისი კურსების საშუალო ფასი).
