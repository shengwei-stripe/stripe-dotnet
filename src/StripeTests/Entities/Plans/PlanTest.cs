namespace StripeTests
{
    using Newtonsoft.Json;
    using Stripe;
    using Xunit;

    public class PlanTest : BaseStripeTest
    {
        public PlanTest(StripeMockFixture stripeMockFixture)
            : base(stripeMockFixture)
        {
        }

        [Fact]
        public void Deserialize()
        {
            string json = this.GetFixture("/v1/plans/plan_123");
            var plan = JsonConvert.DeserializeObject<Plan>(json);
            Assert.NotNull(plan);
            Assert.IsType<Plan>(plan);
            Assert.NotNull(plan.Id);
            Assert.Equal("plan", plan.Object);
        }

        [Fact]
        public void DeserializeWithExpansions()
        {
            string[] expansions =
            {
              "product",
            };

            string json = this.GetFixture("/v1/plans/plan_123", expansions);
            var plan = JsonConvert.DeserializeObject<Plan>(json);
            Assert.NotNull(plan);
            Assert.IsType<Plan>(plan);
            Assert.NotNull(plan.Id);
            Assert.Equal("plan", plan.Object);

            Assert.NotNull(plan.Product);
            Assert.Equal("product", plan.Product.Object);
        }

        // Custom test to ensure PlanTier is deserialized properly
        // TODO: Remove this test on the next major version
        [Fact]
        public void DeserializePlanTier()
        {
            var json = GetResourceAsString("api_fixtures.plan_with_tier.json");
            var plan = JsonConvert.DeserializeObject<Plan>(json);
            Assert.NotNull(plan);
            Assert.IsType<Plan>(plan);
            Assert.NotNull(plan.Id);
            Assert.Equal("plan", plan.Object);
            Assert.Equal(199, plan.Tiers[0].UnitAmountDecimal);
            Assert.Equal(199, plan.Tiers[0].UnitAmountDecinal);
        }
    }
}
