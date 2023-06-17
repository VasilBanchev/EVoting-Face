using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class IDCardPhotosManager : IManager<IDCardPhotos, int>
    {
        private IDCardPhotosContext context;
        public IDCardPhotosManager(VotingDBContext contex)
        {
            this.context = new IDCardPhotosContext(contex);
        }

        public async Task CreateAsync(IDCardPhotos item)
        {
            try
            {
                await context.CreateAsync(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                await context.DeleteAsync(key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeletePhotoesAsync(string key)
        {
            try
            {
                await context.DeletePhotoesAsync(key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<IDCardPhotos>> ReadAllAsync(bool navProp = false)
        {
            try
            {
                return await context.ReadAllAsync(navProp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IDCardPhotos> ReadAsync(int key, bool navProp = false)
        {
            try
            {
                return await context.ReadAsync(key, navProp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateAsync(IDCardPhotos item)
        {
            try
            {
                await context.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


      /*  private async Task<string> RunPythonScript(string scriptPath, string arguments)
        {
            string result;

            using (var process = new Process())
            {
                process.StartInfo.FileName = "python";
                process.StartInfo.Arguments = $"{scriptPath} {arguments}";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();

                using (var reader = process.StandardOutput)
                {
                    result = await reader.ReadToEndAsync();
                }

                process.WaitForExit();
            }

            return result;
        }

        private async Task ExecutePythonScript()
        {
            string scriptPath = "path/to/your/python_script.py";
            string arguments = "arg1 arg2 arg3";
            string result = await RunPythonScript(scriptPath, arguments);
           
        }*/

    }
}
