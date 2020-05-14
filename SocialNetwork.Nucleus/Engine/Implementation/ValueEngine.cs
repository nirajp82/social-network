using SocialNetwork.APIEntity;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo;
using SocialNetwork.Nucleus.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus
{
    internal class ValueEngine : IValueEngine
    {
        #region Members
        private IUnitOfWork _unitOfWork { get; }
        private IMapperHelper _mapperHelper { get; }
        #endregion


        #region Constructors
        public ValueEngine(IUnitOfWork unitOfWork, IMapperHelper mapperHelper)
        {
            _unitOfWork = unitOfWork;
            _mapperHelper = mapperHelper;
        }
        #endregion


        #region Public Methods
        public async Task<IEnumerable<ValueEntity>> FindAllAsync()
        {
            IEnumerable<Value> result = await _unitOfWork.ValueRepository.FindAllAsync();
            return _mapperHelper.MapList<Value, ValueEntity>(result);
        }

        public async Task<ValueEntity> AddAsync(ValueEntity entity)
        {
            Value value = _mapperHelper.Map<ValueEntity, Value>(entity);
            _unitOfWork.ValueRepository.Add(value);
            await _unitOfWork.SaveAsync();
            return _mapperHelper.Map<Value, ValueEntity>(value);

        }

        public async Task UpdateAsync(ValueEntity entity)
        {
            Value value = _mapperHelper.Map<ValueEntity, Value>(entity);
            _unitOfWork.ValueRepository.Update(value);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(long valueId)
        {
            await _unitOfWork.ValueRepository.DeleteAsync(valueId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<ValueEntity> FindFirstAsync(long valueId)
        {
            Value value = await _unitOfWork.ValueRepository.FindFirstAsync(valueId);
            return _mapperHelper.Map<Value, ValueEntity>(value);
        }
        #endregion
    }
}
