using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JG.FinTech.Models
{
    public struct LoginModel : IEquatable<LoginModel>
    {
        /// <summary>
        /// varun
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 4460-210
        /// </summary>
        [Required]
        public string PostCode { get; set; }

        public bool Equals(LoginModel other) => (UserName, PostCode) == (other.UserName, other.PostCode);

        public override bool Equals(object obj) => (obj != null) && Equals(obj);

        public override int GetHashCode() => (UserName, PostCode).GetHashCode();
    }
}
