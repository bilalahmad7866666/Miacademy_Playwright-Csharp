using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using Miacademy_Playwright.Pages;

namespace Miacademy_Playwright.Tests
{
    public class Tests
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IPage _page;
        private HomePage _homePage;
        private ApplicationFormPage _applicationFormPage;

        private const int Timeout = 120000;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            _playwright = await Playwright.CreateAsync();
        }

        [SetUp]
        public async Task Setup()
        {
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            _page = await _browser.NewPageAsync();
            _homePage = new HomePage(_page);
            _applicationFormPage = new ApplicationFormPage(_page);
        }

        [TearDown]
        public async Task Teardown()
        {
            await _page.CloseAsync();
            await _browser.CloseAsync();
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            _playwright.Dispose();
        }

        [Test]
        [Obsolete]
        public async Task ApplyToMiaPrepOnlineHighSchool()
        {
            await _homePage.NavigateTo();

            // Adding detailed logging
            Console.WriteLine("Checking visibility of MiaPrep Online High School link...");
            bool isLinkVisible = await _homePage.IsMiaPrepLinkVisible();
            Console.WriteLine($"MiaPrep Online High School link visible: {isLinkVisible}");

            Assert.That(isLinkVisible, Is.True, "MiaPrep Online High School link is not visible");

            if (isLinkVisible)
            {
                await _homePage.ClickMiaPrepLink();
                await _page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = Timeout });
                Console.WriteLine("Clicked MiaPrep Online High School link and waited for page to load.");
            }

            Assert.That(await _page.IsVisibleAsync("text=Apply to Our School", new PageIsVisibleOptions { Timeout = Timeout }), Is.True, "Apply Now button is not visible");
            await _page.ClickAsync("text=Apply to Our School");
            await _page.GetByLabel("Next Navigates to page 2 out of").ClickAsync();

           await _applicationFormPage.FillParentInformation("Muhammad Bilal", "Ahmed", "Bilal.ahmad7866666@gmail.com", "15735705204");

            await _page.GetByLabel("Next Navigates to page 3 out of").ClickAsync();

            Assert.That(await _page.IsVisibleAsync("text=Student Information", new PageIsVisibleOptions { Timeout = Timeout }), Is.True, "Failed to navigate to the Student Information page.");
        }
    }
    }

