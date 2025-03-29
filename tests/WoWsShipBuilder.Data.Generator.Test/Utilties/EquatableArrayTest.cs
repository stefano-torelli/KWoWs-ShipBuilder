using NUnit.Framework;
using System.Collections.Immutable;
using System.Linq;
using WoWsShipBuilder.Data.Generator.Utilities;

namespace WoWsShipBuilder.Data.Generator.Test.Utilties;

public class EquatableArrayTest
{
    [Test]
    public void EquatableArray_Equals_ReturnsTrueForEqualArrays()
    {
        // Arrange
        var array1 = ImmutableArray.Create(1, 2, 3);
        var array2 = ImmutableArray.Create(1, 2, 3);
        var equatableArray1 = array1.ToEquatableArray();
        var equatableArray2 = array2.ToEquatableArray();

        // Act
        var resultEquatableArray = equatableArray1.Equals(equatableArray2);
        var resultSequenceEqual = array1.SequenceEqual(array2);
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(resultEquatableArray, Is.True);
            Assert.That(resultSequenceEqual, Is.True);
            Assert.That(resultEquatableArray, Is.EqualTo(resultSequenceEqual));
        });
    }

    [Test]
    public void EquatableArray_Equals_ReturnsFalseForDifferentArrays()
    {
        // Arrange
        var array1 = ImmutableArray.Create(1, 2, 3);
        var array2 = ImmutableArray.Create(4, 5, 6);
        var equatableArray1 = array1.ToEquatableArray();
        var equatableArray2 = array2.ToEquatableArray();

        // Act
        var resultEquatableArray = equatableArray1.Equals(equatableArray2);

        // Assert
        Assert.That(resultEquatableArray, Is.False);
    }

    [Test]
    public void EquatableArray_GetHashCode_ReturnsSameHashCodeForEqualArrays()
    {
        // Arrange
        var array1 = ImmutableArray.Create(1, 2, 3);
        var array2 = ImmutableArray.Create(1, 2, 3);
        var equatableArray1 = array1.ToEquatableArray();
        var equatableArray2 = array2.ToEquatableArray();

        // Act
        var hashCodeEquatableArray1 = equatableArray1.GetHashCode();
        var hashCodeEquatableArray2 = equatableArray2.GetHashCode();


        // Assert
        Assert.That(hashCodeEquatableArray1, Is.EqualTo(hashCodeEquatableArray2));
    }

    [Test]
    public void EquatableArray_GetHashCode_ReturnsDifferentHashCodeForDifferentArrays()
    {
        // Arrange
        var array1 = ImmutableArray.Create(1, 2, 3);
        var array2 = ImmutableArray.Create(4, 5, 6);
        var equatableArray1 = array1.ToEquatableArray();
        var equatableArray2 = array2.ToEquatableArray();

        // Act
        var hashCode1 = equatableArray1.GetHashCode();
        var hashCode2 = equatableArray2.GetHashCode();

        // Assert
        Assert.That(hashCode1, Is.Not.EqualTo(hashCode2));
    }
}
