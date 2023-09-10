**How to use**

1- create a new database and name it "ExaminationDB".

2- execute the sql query in the folder "Examination Resources/Schema Script.sql".

3- clone the project and run or publish the project




**Project Description:**

This small project is designed to create, administer, and grade digital examinations.




**Libraries and Design Patterns Used:**

- ASP.NET Core
- SQL Server
- Dapper ORM
- MVC Architecture Pattern: Used for two reasons:
  1. Compliance with the requirement to use MVC in this project.
  2. It offers a well-organized structure suitable for medium-sized projects, reducing implementation time.
- Repository Design Pattern: Implemented to allow easy adaptation to different databases or ORMs in the future without rewriting code.
- AJAX: Used in some requests to enhance the user experience.



**Deployment:**

1- i deployed the project in linux server using Docker"the docker hub image (khamisapis/softwareengineertaskmvc:latest)", and in a windows server using IIS.

2- for security we need to add reverse proxy server like NGINX or Apache to hide the port number and handle the load palancing.




**What Has Been Accomplished:**

1. Created a user-friendly single-page backend and interface for exam management.
2. Supports various question types: Essay, Multiple Choice (with dynamic choices), and True/False questions.
3. Implemented server-side pagination for retrieving all exams.
4. Provided exam deletion functionality.
5. Described the scoring algorithm and implemented a report generation function on the server-side.




**Future Required Implementations:**

1. **Identity for Authentication and Authorization:** Implement user authentication and authorization to protect student data. Utilize role-based authentication to allow administrators to manage exams and reports while enabling students to log in and take exams.

2. **Complete Remaining Functionalities:** Implement the remaining required features, such as exam editing, a student's report viewing interface, and exam-taking capabilities for students.




**Suggested Enhancements:**

1. **Use Entity Framework:** Consider adopting Entity Framework in addition to Dapper. This could save development time and align with best practices in database interactions.

2. **Leverage OpenAI Models:** Explore the integration of OpenAI models to intelligently evaluate essay answers provided by students, enhancing the system's capabilities.

3. **Utilize Power BI for Reporting:** Implement Power BI to generate comprehensive reports analyzing student performance, providing valuable insights.
