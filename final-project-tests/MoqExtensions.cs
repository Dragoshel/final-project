using Moq;
using Microsoft.Extensions.Logging;

namespace FinalProjectTests
{
    public static class MoqExtensions
    {
        // public static Mock<ILogger<T>> VerifyDebugWasCalled<T>(this Mock<ILogger<T>> logger, string expectedMessage)
        // {
        //     Func<object, Type, bool> state = (v, t) => v.ToString().CompareTo(expectedMessage) == 0;

        //     logger.Verify(
        //         x => x.Log(
        //             It.Is<LogLevel>(l => l == LogLevel.Debug),
        //             It.IsAny<EventId>(),
        //             It.Is<It.IsAnyType>((v, t) => state(v, t)),
        //             It.IsAny<Exception>(),
        //             It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));

        //     return logger;
        // }

        // public static Mock<ILogger<T>> VerifyInfoWasCalled<T>(this Mock<ILogger<T>> logger, string expectedMessage)
        // {
        //     Func<object, Type, bool> state = (v, t) => v.ToString().CompareTo(expectedMessage) == 0;

        //     logger.Verify(
        //         x => x.Log(
        //             It.Is<LogLevel>(l => l == LogLevel.Information),
        //             It.IsAny<EventId>(),
        //             It.Is<It.IsAnyType>((v, t) => state(v, t)),
        //             It.IsAny<Exception>(),
        //             It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));

        //     return logger;
        // }
    }
}