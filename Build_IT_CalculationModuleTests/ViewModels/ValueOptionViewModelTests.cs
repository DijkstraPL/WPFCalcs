using Build_IT_CalculationModule.ViewModels;
using Build_IT_CalculationModule.ViewModels.Interfaces;
using Build_IT_Infrastructure.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_CalculationModuleTests.ViewModels
{
    [TestFixture]
    public class ValueOptionViewModelTests
    {
        [Test]
        public void ConstructorTest()
        {
            var parameterData = new Mock<IParameterData>();
            parameterData.Setup(pd => pd.ParameterName).Returns("c");
            parameterData.Setup(pd => pd.ParameterValue).Returns("d");

            var valueOptionViewModel = new ValueOptionViewModel(
                new ValueOptionResource { 
                    Name = "a",
                    Value ="b"
                }, parameterData.Object);

            Assert.Multiple(() =>
            {
                Assert.That(valueOptionViewModel.Name, Is.EqualTo("a"));
                Assert.That(valueOptionViewModel.Value, Is.EqualTo("b"));
                Assert.That(valueOptionViewModel.ParameterName, Is.EqualTo("c"));
            });
        }
        
        [Test]
        public void ConstructorTest_NullValueOptionResource_ThrowsArgumentNullException()
        {
            var parameterData = new Mock<IParameterData>();
            Assert.Throws<ArgumentNullException>(() => new ValueOptionViewModel(null, parameterData.Object));
        }
        [Test]
        public void ConstructorTest_NullParameterData_ThrowsArgumentNullException()
        {
            var parameterData = new Mock<IParameterData>();
            Assert.Throws<ArgumentNullException>(() => new ValueOptionViewModel(new ValueOptionResource(), null));
        }

        [Test]
        [TestCase("d", false)]
        [TestCase("b", true)]
        public void IsOptionCheckedTest_CompareProperValues(string parameterValue, bool expectedIsOptionChecked)
        {
            var parameterData = new Mock<IParameterData>();
            parameterData.Setup(pd => pd.ParameterName).Returns("c");
            parameterData.Setup(pd => pd.ParameterValue).Returns(parameterValue);

            var valueOptionViewModel = new ValueOptionViewModel(
                new ValueOptionResource
                {
                    Name = "a",
                    Value = "b"
                }, parameterData.Object);

                Assert.That(valueOptionViewModel.IsOptionChecked, Is.EqualTo(expectedIsOptionChecked));
        }

        [Test]
        [TestCase(false, false)]
        [TestCase(true, true)]
        public void IsOptionCheckedTest_SetWorksOnlyOnce(bool newIsOptionChecked, bool expectedIsOptionChecked)
        {
            var parameterData = new Mock<IParameterData>();
            parameterData.Setup(pd => pd.ParameterName).Returns("c");
            parameterData.Setup(pd => pd.ParameterValue).Returns("d");
            parameterData.SetupProperty(pd => pd.ParameterValue);

            var valueOptionViewModel = new ValueOptionViewModel(
                new ValueOptionResource
                {
                    Name = "a",
                    Value = "b"
                }, parameterData.Object);

            valueOptionViewModel.IsOptionChecked = newIsOptionChecked;

            Assert.That(valueOptionViewModel.IsOptionChecked, Is.EqualTo(expectedIsOptionChecked));
        }
    }
}
