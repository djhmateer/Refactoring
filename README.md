# Refactoring
For refactoring talk

#0 Source control

-- part1 simple refactoring

#1 use VS tooling to format
  Auto format to make readable
  Remove and tidy unused usings

#2 change names mercilessly!
  To make more understandable
     Variables - noun
     Methods - verb eg writeToLog
     Bool isGood - favour the positive

  Be consistent
    Eg underscores
      namespaces do well

#3 R# - suggestions!
  Eg for each

#4 Comments
  why am doing something…eg 
   Not how…eg 

#5 Dead code
   Must die!

#6 break long methods into smaller
	extract method - ParseLine
	out parameter - codesmell
	extract method - InsertQuoteIntoDatabase

#7 Put in tests!!!
	nuget xunit, xunit.runner
	can we test the static methods (poor mans functional programming?)
		as they have no dependencies

		Suggestion into separate classes

#8 code repeated >2 times refactor 


#9 CQS?

-- part2 design

#10 Favour composition
    why?
     show logging
     show faking out the database to help
     show faking out emailler for when there is an exception
         exception handling

#11 Exception handling bubble up

#12 Log mercilessly to console and prod

Maybe<T> to prevent need for null reference checking

Functional example

