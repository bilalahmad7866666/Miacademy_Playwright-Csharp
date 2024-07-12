using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Miacademy_Playwright.Pages
{
    public class HomePage
    {
        private readonly IPage _page;
        private const int Timeout = 120000;

        public HomePage(IPage page)
        {
            _page = page;
        }

        public async Task NavigateTo()
        {
            int retries = 0;
            const int maxRetries = 3;
            while (true)
            {
                try
                {
                    await _page.GotoAsync("https://miacademy.co/#/", new PageGotoOptions { Timeout = Timeout });
                    await _page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = Timeout });
                    break;
                }
                catch (PlaywrightException ex) when (retries < maxRetries)
                {
                    retries++;
                    Console.WriteLine($"Navigation failed with error: {ex.Message}. Retrying {retries}/{maxRetries}...");
                }
            }
        }

        public async Task<bool> IsMiaPrepLinkVisible()
        {
            var isVisible = await _page.IsVisibleAsync("text=Online High School");
            Console.WriteLine($"Is MiaPrep Online High School link visible: {isVisible}");
            if (!isVisible)
            {
                var pageContent = await _page.ContentAsync();
                Console.WriteLine("Current page content:");
                Console.WriteLine(pageContent);
            }
            return isVisible;
        }

        public async Task ClickMiaPrepLink()
        {
            await _page.ClickAsync("text=Online High School");
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = Timeout });
        }
    }
}
