using SocialNetwork.Dto;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using SocialNetwork.Util;
using System.Collections.Generic;
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
        public async Task<IEnumerable<ValueDto>> FindAllAsync()
        {
            IEnumerable<Value> result = await _unitOfWork.ValueRepo.FindAllAsync();
            return _mapperHelper.MapList<Value, ValueDto>(result);
        }       

        public async Task<ValueDto> AddAsync(ValueDto entity)
        {
            Value value = _mapperHelper.Map<ValueDto, Value>(entity);
            _unitOfWork.ValueRepo.Add(value);
            await _unitOfWork.SaveAsync();
            return _mapperHelper.Map<Value, ValueDto>(value);

        }

        public async Task UpdateAsync(ValueDto entity)
        {
            Value value = _mapperHelper.Map<ValueDto, Value>(entity);
            _unitOfWork.ValueRepo.Update(value);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(long valueId)
        {
            await _unitOfWork.ValueRepo.DeleteAsync(valueId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<ValueDto> FindFirstAsync(long valueId)
        {
            Value value = await _unitOfWork.ValueRepo.FindFirstAsync(valueId);
            return _mapperHelper.Map<Value, ValueDto>(value);
        }

        #endregion
    }
}
