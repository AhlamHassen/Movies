using System;
using Xunit;
using MovieClass;

namespace Test
{
    public class UnitTest1
    {
        [Fact]
        public void TestnumActors()
        { 
            Movie m1 = new Movie(324668, "Jason Bourne", 2016, 123);
            Movie m2 = new Movie(869, "Planet of the Apes", 2001, 119);
            Movie m3 = new Movie(675, "Harry Potter and the Order of the Phoenix", 2007, 138);
            Movie m4 = new Movie(152532, "Dallas Buyers Club", 2013, 117);
            Movie m5 = new Movie(58595, "Snow White and the Huntsman", 2012, 127);


            Assert.Equal(4, m1.numActors(m1.MovieNo));
            Assert.Equal(5, m2.numActors(m2.MovieNo));
            Assert.Equal(4, m3.numActors(m3.MovieNo));
            Assert.Equal(3, m4.numActors(m4.MovieNo));
            Assert.Equal(8, m5.numActors(m5.MovieNo));
        }

        [Fact]
        public void TestgetAge()
        { 
            Movie m1 = new Movie(324668, "Jason Bourne", 2016, 123);
            Movie m2 = new Movie(869, "Planet of the Apes", 2001, 119);
            Movie m3 = new Movie(675, "Harry Potter and the Order of the Phoenix", 2007, 138);
            Movie m4 = new Movie(152532, "Dallas Buyers Club", 2013, 117);
            Movie m5 = new Movie(58595, "Snow White and the Huntsman", 2012, 127);


            Assert.Equal(4, m1.getAge(m1.MovieNo));
            Assert.Equal(19, m2.getAge(m2.MovieNo));
            Assert.Equal(13, m3.getAge(m3.MovieNo));
            Assert.Equal(7, m4.getAge(m4.MovieNo));
            Assert.Equal(8, m5.getAge(m5.MovieNo));
        }
       
    }
}
