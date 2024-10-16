﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class CustomerRepository
    {

        public DataTable ObtenerTodos()
        {
            DataTable dataTable = new DataTable();

            String select = "";
            select = select + "SELECT [CustomerID] " + "\n";
            select = select + "      ,[CompanyName] " + "\n";
            select = select + "      ,[ContactName] " + "\n";
            select = select + "      ,[ContactTitle] " + "\n";
            select = select + "      ,[Address] " + "\n";
            select = select + "      ,[City] " + "\n";
            select = select + "      ,[Region] " + "\n";
            select = select + "      ,[PostalCode] " + "\n";
            select = select + "      ,[Country] " + "\n";
            select = select + "      ,[Phone] " + "\n";
            select = select + "      ,[Fax] " + "\n";
            select = select + "  FROM [dbo].[Customers]";

            SqlDataAdapter adapter = new SqlDataAdapter(select, DataBase.ConnectcionString);
            adapter.Fill(dataTable);
            return dataTable;
        }

        public Customer ObetenerPorId(string id)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                var dataTable = new DataTable();

                String selectForId = "";
                selectForId = selectForId + "SELECT [CustomerID] " + "\n";
                selectForId = selectForId + "      ,[CompanyName] " + "\n";
                selectForId = selectForId + "      ,[ContactName] " + "\n";
                selectForId = selectForId + "      ,[ContactTitle] " + "\n";
                selectForId = selectForId + "      ,[Address] " + "\n";
                selectForId = selectForId + "      ,[City] " + "\n";
                selectForId = selectForId + "      ,[Region] " + "\n";
                selectForId = selectForId + "      ,[PostalCode] " + "\n";
                selectForId = selectForId + "      ,[Country] " + "\n";
                selectForId = selectForId + "      ,[Phone] " + "\n";
                selectForId = selectForId + "      ,[Fax] " + "\n";
                selectForId = selectForId + "  FROM [dbo].[Customers] " + "\n";
                selectForId = selectForId + "  Where CustomerID = @CustomerID";
                using (var comando = new SqlCommand(selectForId, conexion))
                {
                    comando.Parameters.AddWithValue("@CustomerID", id);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    adaptador.Fill(dataTable);
                    Customer cliente = ExtraerInfoCliente(dataTable);
                    return cliente;
                }
            }
        }
        public Customer ExtraerInfoCliente(DataTable dataTeble)
        {
            Customer customer = new Customer();
            foreach (DataRow fila in dataTeble.Rows)
            {
                customer.CustomerID = fila.Field<string>("CustomerID");
                customer.CompanyName = fila.Field<string>("CompanyName");
                customer.ContactName = fila.Field<string>("ContactName");
                customer.ContactTitle = fila.Field<string>("ContactTitle");
                customer.Address = fila.Field<string>("Address");
                customer.City = fila.Field<string>("City");
                customer.Region = fila.Field<string>("Region");
                customer.PostalCode = fila.Field<string>("PostalCode");
                customer.Country = fila.Field<string>("Country");
                customer.Phone = fila.Field<string>("Phone");
                customer.Fax = fila.Field<string>("Fax");
            }
            return customer;
        }

        public int InsertarCliente(Customer cliente)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {

                String InsertarporId = "";
                InsertarporId = InsertarporId + "INSERT INTO [dbo].[Customers] " + "\n";
                InsertarporId = InsertarporId + "           ([CustomerID] " + "\n";
                InsertarporId = InsertarporId + "           ,[CompanyName] " + "\n";
                InsertarporId = InsertarporId + "           ,[ContactName] " + "\n";
                InsertarporId = InsertarporId + "           ,[ContactTitle] " + "\n";
                InsertarporId = InsertarporId + "           ,[Address]) " + "\n";
                InsertarporId = InsertarporId + "     VALUES " + "\n";
                InsertarporId = InsertarporId + "           (@CustomerID " + "\n";
                InsertarporId = InsertarporId + "           ,@CompanyName " + "\n";
                InsertarporId = InsertarporId + "           ,@ContactName " + "\n";
                InsertarporId = InsertarporId + "           ,@ContactTitle " + "\n";
                InsertarporId = InsertarporId + "           ,@Address)";

                using (var commando = new SqlCommand(InsertarporId, conexion))
                {
                    commando.Parameters.AddWithValue("CustomerID", cliente.CustomerID);
                    commando.Parameters.AddWithValue("CompanyName", cliente.CompanyName);
                    commando.Parameters.AddWithValue("ContactName", cliente.ContactName);
                    commando.Parameters.AddWithValue("ContactTitle", cliente.ContactTitle);
                    commando.Parameters.AddWithValue("Address", cliente.Address);
                    SqlDataAdapter adaptador = new SqlDataAdapter(commando);
                    adaptador.InsertCommand = commando;
                    return adaptador.InsertCommand.ExecuteNonQuery();
                }
            }
        }

        public int ActualizarCliente(Customer cliente)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                String updateUser = "";
                updateUser = updateUser + "UPDATE [dbo].[Customers] " + "\n";
                updateUser = updateUser + "   SET [CustomerID] = @CustomerID " + "\n";
                updateUser = updateUser + "      ,[CompanyName] = @CompanyName " + "\n";
                updateUser = updateUser + "      ,[ContactName] = @ContactName " + "\n";
                updateUser = updateUser + "      ,[ContactTitle] = @ContactTitle " + "\n";
                updateUser = updateUser + "      ,[Address] = @Address " + "\n";
                updateUser = updateUser + " WHERE CustomerID = @CustomerID";
                using (var commando = new SqlCommand(updateUser, conexion))
                {
                    SqlCommand comando = parametrosSqlCustomers(commando, cliente);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.UpdateCommand = comando;
                    return adapter.UpdateCommand.ExecuteNonQuery();
                }
            }
        }

        private SqlCommand parametrosSqlCustomers(SqlCommand commando, Customer cliente)
        {
            commando.Parameters.AddWithValue("CustomerID", cliente.CustomerID);
            commando.Parameters.AddWithValue("CompanyName", cliente.CompanyName);
            commando.Parameters.AddWithValue("ContactName", cliente.ContactName);
            commando.Parameters.AddWithValue("ContactTitle", cliente.ContactTitle);
            commando.Parameters.AddWithValue("Address", cliente.Address);
            return commando;
        }

        public int EliminarCliente(string id)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                String deleteCliente = "";
                deleteCliente = deleteCliente + "DELETE FROM [dbo].[Customers] " + "\n";
                deleteCliente = deleteCliente + "      WHERE CustomerID = @CustomerID";
                using (var comando = new SqlCommand(deleteCliente, conexion))
                {
                    comando.Parameters.AddWithValue("@CustomerID", id);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.DeleteCommand = comando;
                    var eliminadas = adapter.DeleteCommand.ExecuteNonQuery();
                    return eliminadas;
                }
            }
        }

    }
}
