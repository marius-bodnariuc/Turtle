using System;

namespace Turtle.ConsoleApp
{
    interface BuilderFromFile<T>
    {
        BuilderWithIOErrorHandler<T> OnIOError(Action<Exception> errorHandler);
        BuilderWithParsingErrorHandler<T> OnParsingError(Action<Exception, string, string> errorHandler);
    }

    interface BuilderWithIOErrorHandler<T>
    {
        BuilderWithIOAndParsingErrorHandler<T> AndOnParsingError(Action<Exception, string, string> errorHandler);
    }

    interface BuilderWithParsingErrorHandler<T>
    {
        BuilderWithIOAndParsingErrorHandler<T> AndOnIOError(Action<Exception> errorHandler);
    }

    interface BuilderWithIOAndParsingErrorHandler<T>
    {
        T Build();
    }

    interface IBuilder<T>
    {
        BuilderWithIOErrorHandler<T> OnIOError(Action<Exception> doThis);
        BuilderWithParsingErrorHandler<T> OnParsingError(Action<Exception, string, string> doThis);

        BuilderWithIOAndParsingErrorHandler<T> AndOnIOError(Action<Exception> doThis);
        BuilderWithIOAndParsingErrorHandler<T> AndOnParsingError(Action<Exception, string, string> doThis);

        T Build();
    }

    abstract class Builder<T> : IBuilder<T>,
        BuilderFromFile<T>,
        BuilderWithIOErrorHandler<T>,
        BuilderWithParsingErrorHandler<T>,
        BuilderWithIOAndParsingErrorHandler<T>
    {
        protected Action<Exception> _onIoError;
        protected Action<Exception, string, string> _onParsingError;

        public BuilderWithIOErrorHandler<T> OnIOError(Action<Exception> doThis)
        {
            _onIoError = doThis;
            return this;
        }

        public BuilderWithParsingErrorHandler<T> OnParsingError(Action<Exception, string, string> doThis)
        {
            _onParsingError = doThis;
            return this;
        }

        public BuilderWithIOAndParsingErrorHandler<T> AndOnIOError(Action<Exception> doThis)
        {
            _onIoError = doThis;
            return this;
        }

        public BuilderWithIOAndParsingErrorHandler<T> AndOnParsingError(Action<Exception, string, string> doThis)
        {
            _onParsingError = doThis;
            return this;
        }

        public abstract T Build();
    }
}