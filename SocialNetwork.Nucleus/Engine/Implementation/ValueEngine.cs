using SocialNetwork.DTO;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using SocialNetwork.Nucleus.Helper;
using SocialNetwork.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus
{
    internal class ValueEngine : IValueEngine
    {
        #region Members
        private ICryptoHelper _cryptoHelper { get; }

        private IUnitOfWork _unitOfWork { get; }
        private IMapperHelper _mapperHelper { get; }
        #endregion


        #region Constructors
        public ValueEngine(IUnitOfWork unitOfWork, IMapperHelper mapperHelper, ICryptoHelper cryptoHelper)
        {
            _unitOfWork = unitOfWork;
            _mapperHelper = mapperHelper;
            _cryptoHelper = cryptoHelper;
        }
        #endregion


        #region Public Methods
        public async Task<IEnumerable<ValueDTO>> FindAllAsync()
        {
            IEnumerable<Value> result = await _unitOfWork.ValueRepo.FindAllAsync();
            return _mapperHelper.MapList<Value, ValueDTO>(result);
        }       

        public async Task<ValueDTO> AddAsync(ValueDTO entity)
        {
            Value value = _mapperHelper.Map<ValueDTO, Value>(entity);
            _unitOfWork.ValueRepo.Add(value);
            await _unitOfWork.SaveAsync();
            return _mapperHelper.Map<Value, ValueDTO>(value);

        }

        public async Task UpdateAsync(ValueDTO entity)
        {
            Value value = _mapperHelper.Map<ValueDTO, Value>(entity);
            _unitOfWork.ValueRepo.Update(value);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(long valueId)
        {
            await _unitOfWork.ValueRepo.DeleteAsync(valueId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<ValueDTO> FindFirstAsync(long valueId)
        {
            Value value = await _unitOfWork.ValueRepo.FindFirstAsync(valueId);
            return _mapperHelper.Map<Value, ValueDTO>(value);
        }
        #endregion

        //private List<Value> NewMethod(IEnumerable<Value> result)
        //{
        //    var list = result.ToList();
        //    list.Clear();
        //    string salt1 = _cryptoHelper.CreateBase64Salt();
        //    string salt2 = _cryptoHelper.CreateBase64Salt();
        //    string salt3 = _cryptoHelper.CreateBase64Salt();
        //    string salt4 = _cryptoHelper.CreateBase64Salt();
        //    string salt5 = _cryptoHelper.CreateBase64Salt();
        //    list.Add(new Value { Name = $"{salt1} ||-|| {_cryptoHelper.GenerateHash("Password1", salt1)}" });
        //    list.Add(new Value { Name = $"{salt2} ||-|| {_cryptoHelper.GenerateHash("Password2", salt2)}" });
        //    list.Add(new Value { Name = $"{salt3} ||-|| {_cryptoHelper.GenerateHash("Password3", salt3)}" });
        //    list.Add(new Value { Name = $"{salt4} ||-|| {_cryptoHelper.GenerateHash("Password4", salt4)}" });
        //    list.Add(new Value { Name = $"{salt5} ||-|| {_cryptoHelper.GenerateHash("Password5", salt5)}" });
        //    return list;
        //}
    }
}
