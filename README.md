# ManyWithDefault

## This is just a conceptual demo, not a real project
I created this project just to explore an idea I've had kicking around in my head for some time now. Essentially, I want to create an application that has entities that can share a common related table.

For example, I want to build an application that has an entity of type "Person," and another entity of type "Organization." In my application, both entities can have a list of e-mail addresses.

I want to use the same e-mail address table from Entity Framework, such that the shared table has a common "ForeignKey_ID" that takes a GUID, and doesn't care about what table the foreign key comes from.

When I try to do simply add related entities to my model using Entity Framework, I get a strange table structure that has a mapped column for each ID (Something like Person_ID and Organization_ID). The more tables I create that use this shared e-mail list, the wider the E-mail table gets. I suppose this doesn't matter, ultimately, but is does not please me for aesthetic reasons. It seems needlessly complex.

I've tried solving the problem using Entity Framework and Fluent API (as described <a href="https://stackoverflow.com/questions/19052860/ef-code-first-single-foreign-key-to-multiple-parents" target="_blank">here</a>), but keep getting error messages about conflicting foreign key relationships. From the thread on Stack Overflow, it looks like I would have to manually override the EF-generated foreign keys, and then would still expect odd problems from time to time. This is not acceptable.

So, I've decided to try to create this some other way. What I have here so far is a bit clunky, but what I have done so far actually works.

(NOTE: For the 9/9/2018 commit, I have created some sample entities called NewEntityPrime and NewEntityAlternate and NewEntitySub for testing purposes. These don't work, and will later be removed).
