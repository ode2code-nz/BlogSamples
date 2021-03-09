# Acceptance Specifications

The acceptance specs are the component tests in Martin Fowler's [microservices testing strategy](https://martinfowler.com/articles/microservice-testing/#testing-component-introduction).

The in-process component tests run in the private build and build pipeline and the same tests can run out-of-process against deployed services in the release pipeline.


This is the home of the acceptance specs.
It is named 'Application' folder because it corresponds with the Application project, which is where application 'Features' are implemented.
New specs will start off in the '_CurrentSprint' folder and then move here at the end of the sprint.
This is the 'living documentation' and so is organised in whatever way creates the most useful and readable documentation.
The audience for this documentation is the customer and whole team and so the specs are written in the ubiquitous language and are hopefully understood by everyone.