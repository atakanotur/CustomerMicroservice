using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Utilities.Business;
using Core.Utilities.Logger;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Validation;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class AddressManager : IAddressService
    {
        IAddressDal _addressDal;

        public AddressManager(IAddressDal addressDal)
        {
            _addressDal = addressDal;
        }
        [ValidationAspect(typeof(AddressValidator))]
        public IDataResult<Address> Add(Address address)
        {
            var result = BusinessRules.Run();
            if (result != null)
            {
                Logger.LogError(Messages.AddressNotCreated, new Exception(Messages.BusinessRulesNotComply));
                return new ErrorDataResult<Address>(Messages.AddressNotCreated);
            }
            _addressDal.Add(address);
            Logger.LogAuditEvent(Messages.AddressCreated);
            return new SuccessDataResult<Address>(Messages.AddressCreated);
        }

        public IResult Delete(Address address)
        {
            var result = BusinessRules.Run();
            if (result != null)
            {
                Logger.LogError(Messages.AddressNotDeleted, new Exception(Messages.BusinessRulesNotComply));
                return new ErrorResult(Messages.AddressNotDeleted);
            }
            _addressDal.Delete(address);
            Logger.LogAuditEvent(Messages.AddressDeleted);
            return new ErrorResult(Messages.AddressNotDeleted);
        }

        public IDataResult<Address> Get(int id)
        {
            var result = BusinessRules.Run();
            if (result != null)
            {
                Logger.LogError(Messages.AddressesNotListed, new Exception(Messages.BusinessRulesNotComply));
                return new ErrorDataResult<Address>(Messages.AddressesNotListed);
            }
            Logger.LogAuditEvent(Messages.AddressesListed);
            return new SuccessDataResult<Address>(_addressDal.Get(c => c.Id == id), Messages.AddressesListed);
        }

        public IDataResult<List<Address>> GetAll()
        {
            var result = BusinessRules.Run();
            if (result != null)
            {
                Logger.LogError(Messages.AddressesNotListed, new Exception(Messages.BusinessRulesNotComply));
                return new ErrorDataResult<List<Address>>(Messages.AddressesNotListed);
            }
            var addresses = _addressDal.GetAll();
            Logger.LogAuditEvent(Messages.AddressesListed);
            return new SuccessDataResult<List<Address>>(_addressDal.GetAll(), Messages.AddressesNotListed);
        }

        public IResult Update(Address address)
        {
            var result = BusinessRules.Run();
            if (result != null)
            {
                Logger.LogError(Messages.AddressNotUpdated, new Exception(Messages.BusinessRulesNotComply));
                return new ErrorResult(Messages.AddressNotUpdated);
            }
            _addressDal.Update(address);
            Logger.LogAuditEvent(Messages.AddressUpdated);
            return new SuccessResult(Messages.AddressUpdated);
        }
    }
}
