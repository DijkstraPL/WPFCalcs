using Build_IT_CalculationModule.ViewModels;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;
using Build_IT_Infrastructure.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_CalculationModuleTests.ViewModels
{
    [TestFixture]
    public class ParameterControlViewModelTests
    {
        [Test]
        public void ConstructorTest()
        {
            var parameterControlViewModel = new ParameterControlViewModel(new ParameterResource()
            {
                Name = "a",
                Value = "b",
                Unit = "c",
                ValueOptions = new List<ValueOptionResource>
                {
                    new ValueOptionResource(),
                    new ValueOptionResource()
                }
            });

            Assert.Multiple(() =>
            {
                Assert.That(parameterControlViewModel.ParameterResource, Is.Not.Null);
                Assert.That(parameterControlViewModel.ParameterName, Is.EqualTo("a"));
                Assert.That(parameterControlViewModel.ParameterValue, Is.EqualTo("b"));
                Assert.That(parameterControlViewModel.ParameterUnit, Is.EqualTo("c"));
                Assert.That(parameterControlViewModel.ValueOptions.Count(), Is.EqualTo(2));
                Assert.IsTrue(parameterControlViewModel.IsVisible);
                Assert.IsTrue(parameterControlViewModel.IsValid);
                Assert.IsTrue(parameterControlViewModel.IsClean);
            });
        }

        [Test]
        public void ConstructorTest_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ParameterControlViewModel(null));
        }

        [Test]
        [TestCase("aa_bb_cc", "aa", "bb", null, "cc")]
        [TestCase("aa^bb^_cc_dd", "aa", "cc", "bb", "dd")]
        public void SetNamesTest(string name, string expectedParameterNameMain,
            string expectedParameterNameSubscript, string expectedParameterNameSupscript,
            string expectedParameterNameLast)
        {
            var parameterControlViewModel = new ParameterControlViewModel(new ParameterResource()
            {
                Name = name,
            });

            Assert.Multiple(() =>
            {
                Assert.That(parameterControlViewModel.ParameterNameMain, Is.EqualTo(expectedParameterNameMain));
                Assert.That(parameterControlViewModel.ParameterNameSubscript, Is.EqualTo(expectedParameterNameSubscript));
                Assert.That(parameterControlViewModel.ParameterNameSupscript, Is.EqualTo(expectedParameterNameSupscript));
                Assert.That(parameterControlViewModel.ParameterNameLast, Is.EqualTo(expectedParameterNameLast));
            });
        }

        [Test]
        public void IsBooleanTest_True()
        {
            var parameterControlViewModel = new ParameterControlViewModel(new ParameterResource()
            {
                Name = "a",
                ValueOptionSetting = ValueOptionSettings.Boolean,
            });

            Assert.Multiple(() =>
            {
                Assert.IsTrue(parameterControlViewModel.IsBoolean);
                Assert.IsFalse(parameterControlViewModel.IsEditable);
                Assert.IsFalse(parameterControlViewModel.ContainsValueOptions);
                Assert.IsFalse(parameterControlViewModel.ShouldUseRadioButtons);
                Assert.IsTrue(parameterControlViewModel.IsRequired);
            });
        }

        [Test]
        public void IsEditableTest_True()
        {
            var parameterControlViewModel = new ParameterControlViewModel(new ParameterResource()
            {
                Name = "a",
                ValueOptionSetting = ValueOptionSettings.UserInput,
            });

            Assert.Multiple(() =>
            {
                Assert.IsTrue(parameterControlViewModel.IsEditable);
                Assert.IsFalse(parameterControlViewModel.IsBoolean);
                Assert.IsFalse(parameterControlViewModel.ContainsValueOptions);
                Assert.IsFalse(parameterControlViewModel.ShouldUseRadioButtons);
                Assert.IsTrue(parameterControlViewModel.IsRequired);
            });
        }

        [Test]
        public void ContainsValueOptionsTest_True()
        {
            var parameterControlViewModel = new ParameterControlViewModel(new ParameterResource()
            {
                Name = "a",
                ValueOptions = new List<ValueOptionResource>
                {
                    new ValueOptionResource(),
                    new ValueOptionResource(),
                    new ValueOptionResource(),
                    new ValueOptionResource(),
                }
            });

            Assert.Multiple(() =>
            {
                Assert.IsTrue(parameterControlViewModel.ContainsValueOptions);
                Assert.IsFalse(parameterControlViewModel.ShouldUseRadioButtons);
                Assert.IsFalse(parameterControlViewModel.IsEditable);
                Assert.IsFalse(parameterControlViewModel.IsBoolean);
                Assert.IsTrue(parameterControlViewModel.IsRequired);
            });
        }

        [Test]
        public void ContainsFewValueOptionsTest_True()
        {
            var parameterControlViewModel = new ParameterControlViewModel(new ParameterResource()
            {
                Name = "a",
                ValueOptions = new List<ValueOptionResource>
                {
                    new ValueOptionResource(),
                    new ValueOptionResource(),
                }
            });

            Assert.Multiple(() =>
            {
                Assert.IsTrue(parameterControlViewModel.ContainsValueOptions);
                Assert.IsTrue(parameterControlViewModel.ShouldUseRadioButtons);
                Assert.IsFalse(parameterControlViewModel.IsEditable);
                Assert.IsFalse(parameterControlViewModel.IsBoolean);
                Assert.IsTrue(parameterControlViewModel.IsRequired);
            });
        }

        [Test]
        public void IsRequiredTest_False()
        {
            var parameterControlViewModel = new ParameterControlViewModel(new ParameterResource()
            {
                Name = "a",
                Context = ParameterOptions.Optional,
            });

            Assert.Multiple(() =>
            {
                Assert.IsFalse(parameterControlViewModel.IsEditable);
                Assert.IsFalse(parameterControlViewModel.IsBoolean);
                Assert.IsFalse(parameterControlViewModel.ContainsValueOptions);
                Assert.IsFalse(parameterControlViewModel.ShouldUseRadioButtons);
                Assert.IsFalse(parameterControlViewModel.IsRequired);
            });
        }

        [Test]
        [TestCase(true, "true", true)]
        [TestCase(false, "false", false)]
        public void IsValueCheckedTest(bool isValueChecked, string parameterValue, bool expectedIsValueChecked)
        {
            var parameterControlViewModel = new ParameterControlViewModel(new ParameterResource()
            {
                Name = "a",
            });

            parameterControlViewModel.IsValueChecked = isValueChecked;

            Assert.That(parameterControlViewModel.ParameterValue, Is.EqualTo(parameterValue));
            Assert.That(parameterControlViewModel.IsValueChecked, Is.EqualTo(expectedIsValueChecked));
        }

        [Test]
        [TestCase(true, null, true)]
        [TestCase(false, "false", false)]
        public void IsDefaultValueCheckedTest(bool isDefaultValueChecked, string parameterValue, bool expectedIsDefaultValueChecked)
        {
            var parameterControlViewModel = new ParameterControlViewModel(new ParameterResource()
            {
                Name = "a",
            });

            parameterControlViewModel.IsDefaultValueChecked = isDefaultValueChecked;

            Assert.That(parameterControlViewModel.ParameterValue, Is.EqualTo(parameterValue));
            Assert.That(parameterControlViewModel.IsDefaultValueChecked, Is.EqualTo(expectedIsDefaultValueChecked));
        }

        [Test]
        public void ParameterValueTest_IsCleanSouldBeFalse()
        {
            var parameterControlViewModel = new ParameterControlViewModel(new ParameterResource()
            {
                Name = "a",
            });

            parameterControlViewModel.ParameterValue = "b";

            Assert.That(parameterControlViewModel.ParameterValue, Is.EqualTo("b"));
            Assert.IsFalse(parameterControlViewModel.IsClean);
        }

        [Test]
        public void ParameterValueTest_EventShouldBeRaised()
        {
            var parameterControlViewModel = new ParameterControlViewModel(new ParameterResource()
            {
                Name = "a",
            });

            var wasCalled = false;
            parameterControlViewModel.ValueChanged += (o, e) => wasCalled = true;

            parameterControlViewModel.ParameterValue = "b";

            Assert.IsTrue(wasCalled);
        }
    }
}