namespace multi_hosp_demo.MultiHosp
{
    public interface IMultiHospProvider
    {
        string GetHospCode();
        void SetHospCode(string hospCode);
    }
    class MultiHospProvider : IMultiHospProvider
    {
        private string _hospCode;
        public string GetHospCode()
        {
            return _hospCode;
        }

        public void SetHospCode(string hospCode)
        {
            _hospCode = hospCode;
        }
    }
}