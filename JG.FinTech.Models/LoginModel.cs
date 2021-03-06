﻿namespace JG.FinTech.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

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
