using FirstTaskEntities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskEntities.Models
{
	public class ServiceProvided
	{
		// Добавить таблицу в БД и репозиторий
		public int Id { get; set; }
		public int ServiceId { get; set; }
		public Company Company { get; set; }
		public DateTime DateOfBegin { get; set; }
		public DateTime DateOfFinish { get; set; }
		public ServiceStatuses Status { get; set; }
	}
}
