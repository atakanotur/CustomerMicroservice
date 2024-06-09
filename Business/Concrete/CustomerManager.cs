using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Core.Utilities.Logger;
using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        IAddressService _addressService;

        public CustomerManager(ICustomerDal customerDal, IAddressService addressService)
        {
            _customerDal = customerDal;
            _addressService = addressService;
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(CustomerDetailDto customerDetail)
        {
            var result = BusinessRules.Run(CheckIfCustomerIsAlreadyExist(customerDetail.Email));
            if (result != null)
            {
                Logger.LogError(Messages.CustomerNotCreated, new Exception(Messages.BusinessRulesNotComply));
                return new ErrorResult(Messages.CustomerNotCreated);
            }
            var address = new Address
            {
                AddressLine = customerDetail.AddressLine,
                City = customerDetail.City,
                CityCode = customerDetail.CityCode,
                Country = customerDetail.Country,
            };
            var newAddress = _addressService.Add(address);
            var customer = new Customer
            {
                AddressId = newAddress.Data.Id,
                Email = customerDetail.Email,
                Name = customerDetail.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            _customerDal.Add(customer);
            Logger.LogAuditEvent(Messages.CustomerCreated);
            return new SuccessResult(Messages.CustomerCreated);
        }

        public IResult Delete(Customer customer)
        {
            var result = BusinessRules.Run();
            if (result != null)
            {
                Logger.LogError(Messages.CustomerNotDeleted, new Exception(Messages.BusinessRulesNotComply));
                return new ErrorResult(Messages.CustomerNotDeleted);
            }
            _customerDal.Delete(customer);
            Logger.LogAuditEvent(Messages.CustomerDeleted);
            return new ErrorResult(Messages.CustomerNotDeleted);
        }

        public IDataResult<Customer> GetById(int id)
        {
            var result = BusinessRules.Run();
            if (result != null)
            {
                Logger.LogError(Messages.CustomersNotListed, new Exception(Messages.BusinessRulesNotComply));
                return new ErrorDataResult<Customer>(Messages.CustomersNotListed);
            }
            Logger.LogAuditEvent(Messages.CustomersListed);
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.Id == id), Messages.CustomersListed);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            var result = BusinessRules.Run();
            if (result != null)
            {
                Logger.LogError(Messages.CustomersNotListed, new Exception(Messages.BusinessRulesNotComply));
                return new ErrorDataResult<List<Customer>>(Messages.CustomersNotListed);
            }
            var customers = _customerDal.GetAll();
            Logger.LogAuditEvent(Messages.CustomersListed);
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.CustomersListed);
        }

        public IResult Update(Customer customer)
        {
            var result = BusinessRules.Run();
            if (result != null)
            {
                Logger.LogError(Messages.CustomerNotUpdated, new Exception(Messages.BusinessRulesNotComply));
                return new ErrorResult(Messages.CustomerNotUpdated);
            }
            _customerDal.Update(customer);
            Logger.LogAuditEvent(Messages.CustomerUpdated);
            return new SuccessResult(Messages.CustomerUpdated);
        }

        public IResult Validate(int id)
        {
            var result = BusinessRules.Run();
            if (result != null)
            {
                Logger.LogError(Messages.CustomerNotValidated, new Exception(Messages.BusinessRulesNotComply));
                return new ErrorResult(Messages.CustomerNotValidated);
            }
            Logger.LogAuditEvent(Messages.CustomerValidated);
            return new SuccessResult(Messages.CustomerValidated);
        }

        private IResult CheckIfCustomerIsAlreadyExist(string email)
        {
            var customer = _customerDal.Get(c => c.Email == email);
            if (customer != null) return new ErrorResult(Messages.CustomerAlreadyExist);
            return new SuccessResult();
        }
    }
}
