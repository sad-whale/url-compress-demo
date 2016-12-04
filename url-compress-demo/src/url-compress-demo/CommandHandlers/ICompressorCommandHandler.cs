using url_compress_demo.Commands;

namespace url_compress_demo.CommandHandlers
{
    /// <summary>
    /// интерфейс обработчика команд - моя попытка придерживаться принципов CQRS
    /// </summary>
    public interface ICompressorCommandHandler
    {
        /// <summary>
        /// Выполнить команду оздания новой "сжатой" ссылки
        /// </summary>
        /// <param name="command">данные команды</param>
        void Handle(CompressUrlCommand command);

        /// <summary>
        /// Выполнить команду инкрементирования счетчика переходов
        /// </summary>
        /// <param name="command">данные команды</param>
        void Handle(IncrementClickCountCommand command);
    }
}
