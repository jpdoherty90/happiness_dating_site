# happiness_dating_site

  This was a project I built with two of my friends from Coding Dojo, Erick Ruiz and Samantha Zoeller.  
We were aiming to build an online dating web app that matches singles to other singles, giving them a 
match percentage and allowing them to message one another.  The project was a one-week sprint.  The first
day we sat down as a team and white-boarded the models we wanted to use and the basics of how we wanted to
match users.  We then split off and tackled segments of the project on our own.

  My first major responsibility of the week was writing the matching algorithm, which takes in the IDs of
two users as arguments.  If they are a match (over a threshhold percentage of their interests and traits
align) then a new instace of the Match class is instatiated with a percentage attribute telling what
their match percentage is.  In writing this algorithm I had to work closely with Erick, who was writing 
the front end forms the users fill out as they are registering and detailing their preferences.

  The other major piece of functionality I built was messaging between users.  I set it up so that only 
matcehd users could message one another, and we built the front-end of messaging to look similar to the 
familiar iPhone text message UI.

  Finally I'd like to mention that while we poured our hearts and souls into this project, it was still
above all else a learning experience and not a deliverable final product.  It was our first experience
building a project in C# and ASP.NET.  I have been learning and will continue to learn about OOP, design
patterns, and best practices, and I'm sure if I refactor this project in a few months there will be a lot
of changes to be made.  That is the nature of learning and getting better.  
