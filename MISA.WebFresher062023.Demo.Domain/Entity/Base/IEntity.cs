namespace MISA.WebFresher062023.Demo.Domain
{
    public interface IEntity<Tkey>
    {
        /// <summary>
        /// lấy id của IEntity
        /// </summary>
        /// <returns></returns>
        Tkey GetId();

        /// <summary>
        /// lấy code của IEntity
        /// </summary>
        /// <returns></returns>
        string GetCode();

        /// <summary>
        /// set id cho IEntity
        /// </summary>
        /// <param name="id"></param>
        void SetId(Tkey id);

        /// <summary>
        /// set code cho IEntity
        /// </summary>
        /// <param name="code"></param>
        void SetCode(string code);
    }
}
