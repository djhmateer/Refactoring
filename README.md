# Refactoring Basics
davemateer@gmail.com

github.com/djhmateer/refactoring (link at end too)

##What is Refactoring?
>Refactoring is the process of making code better
>without modifying its external behavior. 

###What do I mean as 'better'?
- Easier to understand (less WTF's per minute)
- Easier to test
- More pleasing

# Part 1 - Simple Refactoring Techniques

###0 Source control

###1 VS tooling to format
  Auto format to make readable
  Remove and tidy unused usings

###2 Rename mercilessly!
  To make more understandable
     Variables - noun
     Methods - verb eg writeToLog
     Bool isGood - favour the positive

  Be consistent
    Eg underscores
      namespaces do well

###3 R# - suggestions!
  Eg for each

###4 Comments
  why am doing something…eg 
   Not how…eg 

###5 Dead code
   Must die!

###6 Break long methods into smaller
	extract method - ParseLine
	out parameter - codesmell
	connectionString

	extract method - InsertQuoteIntoDatabase

###7 Put in tests!!!
	nuget xunit, xunit.runner
	can we test the static methods (poor mans functional programming?)
		as they have no dependencies

		Suggestion into separate classes

###8 code repeated >2 times refactor 


# Part2 - design

###10 Favour composition
    why?
     show logging
     show faking out the database to help
     show faking out emailler for when there is an exception
         exception handling

###11 Exception handling bubble up

###12 Log mercilessly to console and prod


# End Summary
- Use Source Control
- Rename mercilessly!
- Make things smaller
- Test
- Favour composition

www.github.com/djhmateer/refactoring



# Thoughts

### Show other programmers your code!
  The act of explaining...

###12.5 CQS?

###13 Maybe<T> to prevent need for null reference checking

###14 Do in a Functional style with higher order functions

