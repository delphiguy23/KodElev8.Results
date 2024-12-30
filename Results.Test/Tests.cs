namespace Results.Test;

public class Tests
{
    [Fact]
    public void ResultIsSuccessIfThereIsSomething()
    {
        var result = 1.ToResults();
        result.IsSuccess().Should().BeTrue();
        result.Value.Should().Be(1);
    }

    [Fact]
    public void ResultIsNullThenItShouldBeNotFound()
    {
        string? _results = null;

        var result = _results.ToResults();
        result.IsNotFoundOrBadRequest().Should().BeTrue();
    }

    [Fact]
    public void ResultExceptionThenFailure()
    {
        IResults<int> results = ResultsTo.Something<int>(10);
        try
        {
            var _result = results.Value / 0; //trigger divide by zero exception
        }
        catch (Exception ex)
        {
            results = ResultsTo.Failure<int>().FromException(ex);
        }

        results.IsFailure().Should().BeTrue();
    }


    [Fact]
    public void ResultSuccessThenOk()
    {
        IResults<int> AddNumber(int a, int b)
        {
            return ResultsTo.Something<int>(a + b);
        }

        var results = AddNumber(1, 2);

        results.IsSuccess().Should().BeTrue();
        results.Value.Should().Be(3);
    }

    [Fact]
    public void ToResultsThenToSomething()
    {
        var someValue = 10;

        var result = someValue.ToResults();

        result.IsSuccess().Should().BeTrue();
        result.Value.Should().BeOfType(typeof(int));
    }

    [Fact]
    public void FromSomethingThenToIs()
    {
        var someValue = 10;
        var _result = ResultsTo.Failure<int>("Initial failure");
        ;

        var result1 = someValue.ToResults();
        var result2 = result1.Is(x => x > 5);
        var result3 = _result.Is(x => x > 5);

        result1.IsSuccess().Should().BeTrue();
        result2.IsSuccess().Should().BeTrue();
        result2.Value.Should().Be(someValue);
        result3.IsSuccess().Should().BeFalse();
    }

    [Fact]
    public void FromSomethingThenToIs1()
    {
        string? text = null;

        var result = ResultsTo.Success<string>(null);
        var _result = result.Is(x => !string.IsNullOrEmpty(x));

        _result.IsFailure().Should().BeTrue();
        _result.Messages.Should().Contain("Value is null");
    }

    [Fact]
    public void WhenSomethingIsResultsToThen()
    {
        var someValue = 10;

        var result1 = someValue.ToResults();
        var result2 = result1.Is<int, int>(x => x > 5, x => ResultsTo.Something(x.Value * 2));

        result2.IsSuccess().Should().BeTrue();
        result2.Value.Should().Be(someValue * 2);
    }

    [Fact]
    public void WhenSomethingIsResultsToThenOrElse1()
    {
        var someValue = 10;

        var result1 = someValue.ToResults();
        var result2 = result1.Is<int, int>(
            x => x > 5,
            x => ResultsTo.Something(x.Value * 2),
            x => ResultsTo.Something(x.Value - 5));

        result2.IsSuccess().Should().BeTrue();
        result2.Value.Should().Be(someValue * 2);
    }

    [Fact]
    public void WhenSomethingIsResultsToThenOrElse2()
    {
        var someValue = 5;

        var result1 = someValue.ToResults();
        var result2 = result1.Is<int, int>(
            x => x > 5,
            x => ResultsTo.Something(x.Value * 2),
            x => ResultsTo.Something(x.Value - 5));

        result2.IsSuccess().Should().BeTrue();
        result2.Value.Should().Be(someValue - 5);
    }

    [Fact]
    public void WhenValidatePassThenSuccess()
    {
        var someValue = 10;

        var result1 = someValue.ToResults();
        var result2 = result1.Validate<int>(x => x < 10, "Value cant be less than 10");

        result2.IsFailure().Should().BeFalse();
    }

    [Fact]
    public void WhenValidateFailThenFailure()
    {
        var someValue = 9;

        var result1 = someValue.ToResults();
        var result2 = result1.Validate<int>(x => x < 10, "Value cant be less than 10");

        result2.IsFailure().Should().BeTrue();
        result2.Messages.Should().Contain("Value cant be less than 10");
    }

    [Fact]
    public void WhenNonIResultValidatePassThenSuccess()
    {
        var someValue = 10;

        var result2 = someValue.Validate<int>(x => x < 10, "Value cant be less than 10");

        result2.IsFailure().Should().BeFalse();
    }

    [Fact]
    public void WhenNonIResultValidateFailThenFailure()
    {
        var someValue = 9;

        var result1 = someValue.ToResults();
        var result2 = result1.Validate<int>(x => x < 10, "Value cant be less than 10");

        result2.IsFailure().Should().BeTrue();
        result2.Messages.Should().Contain("Value cant be less than 10");
    }

    [Fact]
    public void WhenNestedValidatePassThenSuccess10()
    {
        var someValue = 20;

        var result2 = someValue
            .Validate(x => x < 10, "Value cant be less than 10")
            .Validate(x => x < 15, "Value cant be less than 15")
            .Validate(x => x < 20, "Value cant be less than 20");

        result2.IsSuccess().Should().BeTrue();
        result2.Value.Should().Be(someValue);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(20)]
    public void WhenNestedValidateFailThenFailure10(int someValue)
    {
        var result2 = someValue
            .Validate(x => x <= 10, "Value cant be less than or equal 10")
            .Validate(x => x <= 15, "Value cant be less than or equal 15")
            .Validate(x => x <= 20, "Value cant be less than or equal 20");

        result2.IsFailure().Should().BeTrue();
        result2.Messages.Should().Contain($"Value cant be less than or equal {someValue}");
    }

    [Theory]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(20)]
    [InlineData(21)]
    public void WhenNestedValidatePasslThenContinueChain(int someValue)
    {
        var result2 = someValue
            .Validate(x => x <= 10, "Value cant be less than or equal 10")
            .Validate(x => x <= 15, "Value cant be less than or equal 15")
            .Validate(x => x <= 20, "Value cant be less than or equal 20")
            .Then(x => x * 2);

        if (someValue < 21)
        {
            result2.IsFailure().Should().BeTrue();
            result2.Messages.Should().Contain($"Value cant be less than or equal {someValue}");
        }
        else
        {
            result2.IsSuccess().Should().BeTrue();
            result2.Value.Should().Be(someValue * 2);
        }
    }

    [Theory]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(20)]
    [InlineData(22)]
    [InlineData(25)]
    public void WhenNestedValidatePasslThenContinueThenChain(int someValue)
    {
        var result2 = someValue
            .Validate(x => x <= 10, "Value cant be less than or equal 10")
            .Validate(x => x <= 15, "Value cant be less than or equal 15")
            .Validate(x => x <= 20, "Value cant be less than or equal 20")
            .Is<int, string>(x => (x * 2) < 50, x => "Product is less than 50".ToResults(),
                x => "Product is more than 50".ToResults());

        if (someValue < 21)
        {
            result2.IsFailure().Should().BeTrue();
            result2.Messages.Should().Contain($"Value cant be less than or equal {someValue}");
        }
        else
        {
            result2.IsSuccess().Should().BeTrue();

            if (someValue * 2 < 50)
            {
                result2.Value.Should().Contain("Product is less than 50");
            }
            else
            {
                result2.Value.Should().Contain("Product is more than 50");
            }
        }
    }

    [Fact]
    public void WhenStatusIsSuccessThenReturnIsSuccess()
    {
        var mockResult = new Mock<IResults>();
        mockResult.Setup(r => r.Status).Returns(ResultsStatus.Success);

        var isSuccess = mockResult.Object.IsSuccess();

        isSuccess.Should().BeTrue();
    }

    [Fact]
    public void WhenStatusIsCreatedThenReturnIsCreated()
    {
        var mockResult = new Mock<IResults>();
        mockResult.Setup(r => r.Status).Returns(ResultsStatus.Created);

        var isCreated = mockResult.Object.IsCreated();

        isCreated.Should().BeTrue();
    }

    [Fact]
    public void WhenStatusIsUpdatedThenReturnIsUpdated()
    {
        var mockResult = new Mock<IResults>();
        mockResult.Setup(r => r.Status).Returns(ResultsStatus.Updated);

        var isUpdated = mockResult.Object.IsUpdated();

        isUpdated.Should().BeTrue();
    }

    [Fact]
    public void WhenStatusIsDeletedThenReturnIsDeleted()
    {
        var mockResult = new Mock<IResults>();
        mockResult.Setup(r => r.Status).Returns(ResultsStatus.Deleted);

        var isDeleted = mockResult.Object.IsDeleted();

        isDeleted.Should().BeTrue();
    }

    [Fact]
    public void WhenStatusIsNotFoundThenReturnIsNotFound()
    {
        var mockResult = new Mock<IResults>();
        mockResult.Setup(r => r.Status).Returns(ResultsStatus.NotFound);

        var isNotFound = mockResult.Object.IsNotFound();

        isNotFound.Should().BeTrue();
    }

    [Fact]
    public void WhenStatusIsBadRequestThenReturnIsBadRequest()
    {
        var mockResult = new Mock<IResults>();
        mockResult.Setup(r => r.Status).Returns(ResultsStatus.BadRequest);

        var isBadRequest = mockResult.Object.IsBadRequest();

        isBadRequest.Should().BeTrue();
    }

    [Fact]
    public void WhenStatusIsNotFoundOrBadRequestThenReturnIsNotFoundOrBadRequest1()
    {
        var mockResult = new Mock<IResults>();
        mockResult.Setup(r => r.Status).Returns(ResultsStatus.NotFound);

        var isNotFoundOrBadRequest = mockResult.Object.IsNotFoundOrBadRequest();

        isNotFoundOrBadRequest.Should().BeTrue();
    }

    [Fact]
    public void WhenStatusIsNotFoundOrBadRequestThenReturnIsNotFoundOrBadRequest2()
    {
        var mockResult = new Mock<IResults>();
        mockResult.Setup(r => r.Status).Returns(ResultsStatus.BadRequest);

        var isNotFoundOrBadRequest = mockResult.Object.IsNotFoundOrBadRequest();

        isNotFoundOrBadRequest.Should().BeTrue();
    }

    [Fact]
    public void OnNotFound_ShouldInvokeFunc_WhenResultIsNotFound()
    {
        var mockResult = new Mock<IResults>();
        mockResult.Setup(r => r.Status).Returns(ResultsStatus.NotFound);

        var mockFunc = new Mock<Func<IResults>>();
        var expectedResult = new Mock<IResults>();
        mockFunc.Setup(f => f()).Returns(expectedResult.Object);

        var actual = mockResult.Object.OnNotFound(mockFunc.Object);

        mockFunc.Verify(f => f(), Times.Once);
        actual.Should().Be(expectedResult.Object);
    }

    [Fact]
    public void OnNotFound_ShouldReturnOriginalResult_WhenResultIsNotNotFound()
    {
        var mockResult = new Mock<IResults>();
        mockResult.Setup(r => r.Status).Returns(ResultsStatus.Success);

        var mockFunc = new Mock<Func<IResults>>();

        var actual = mockResult.Object.OnNotFound(mockFunc.Object);

        mockFunc.Verify(f => f(), Times.Never);
        actual.Should().Be(mockResult.Object);
    }

}
