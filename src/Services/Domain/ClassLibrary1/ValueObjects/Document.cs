using PicPaySimplificado.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Domain.ValueObjects
{
    public class Document : ValueObject
    {
        public const int CPF_MAX_LENGTH = 11;
        public const int CNPJ_MAX_LENGTH = 14;
        public string Number { get; private set; }

        public Document(string number, int type)
        {
            ValidateDocument(number, type);
        }

        private void ValidateDocument(string number, int type)
        {
            if(type == 1)
            {
                if (!ValidateCnpj(number))
                {
                    throw new DomainException(DomainExceptionMessages.DocumentValidateErrorMessage);
                }
                Number = number;
            }else
            {
                if(!ValidateCpf(number))
                {
                    throw new DomainException(DomainExceptionMessages.DocumentValidateErrorMessage);
                }
                Number = number;
            }
        }

        private bool ValidateCpf(string cpf)
        {
            cpf = cpf.OnlyNumbers(cpf);

            if (cpf.Length > CPF_MAX_LENGTH)
                return false;

            while (cpf.Length != CPF_MAX_LENGTH)
                cpf = '0' + cpf;

            var igual = true;
            for (var i = 1; i < CPF_MAX_LENGTH && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            var numeros = new int[CPF_MAX_LENGTH];

            for (var i = 0; i < CPF_MAX_LENGTH; i++)
                numeros[i] = int.Parse(cpf[i].ToString());

            var soma = 0;
            for (var i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            var resultado = soma % CPF_MAX_LENGTH;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != CPF_MAX_LENGTH - resultado)
                return false;

            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += (CPF_MAX_LENGTH - i) * numeros[i];

            resultado = soma % CPF_MAX_LENGTH;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else if (numeros[10] != CPF_MAX_LENGTH - resultado)
                return false;

            return true;
        }

        private bool ValidateCnpj(string cnpj)
        {
            cnpj = cnpj.OnlyNumbers(cnpj);

            if (cnpj.Length != CNPJ_MAX_LENGTH)
                return false;

            if (cnpj.All(digit => digit == cnpj[0]))
                return false;

            int[] multiplicadores1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int sum = 0;

            for (int i = 0; i < 12; i++)
            {
                sum += int.Parse(tempCnpj[i].ToString()) * multiplicadores1[i];
            }

            int remainder = sum % 11;
            remainder = remainder < 2 ? 0 : 11 - remainder;
            string digit = remainder.ToString();

            tempCnpj += digit;

            sum = 0;

            for (int i = 0; i < 13; i++)
            {
                sum += int.Parse(tempCnpj[i].ToString()) * multiplicadores2[i];
            }

            remainder = sum % 11;
            remainder = remainder < 2 ? 0 : 11 - remainder;
            digit += remainder;

            return cnpj.EndsWith(digit);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
