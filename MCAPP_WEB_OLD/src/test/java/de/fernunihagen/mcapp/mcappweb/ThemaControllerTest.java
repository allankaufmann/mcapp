package de.fernunihagen.mcapp.mcappweb;

import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;
import org.springframework.test.context.web.WebAppConfiguration;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.web.context.WebApplicationContext;

import static org.springframework.restdocs.mockmvc.RestDocumentationRequestBuilders.post;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.content;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;
import static org.springframework.test.web.servlet.setup.MockMvcBuilders.webAppContextSetup;

@RunWith(SpringJUnit4ClassRunner.class)
@WebAppConfiguration
@ContextConfiguration(classes = { McappwebApplication.class })
public class ThemaControllerTest {
	protected MockMvc mockMvc;
	@Autowired
	private WebApplicationContext webApplicationContext;

	@Before
	public final void initMockMvc() throws Exception {
		mockMvc = webAppContextSetup(webApplicationContext).build();
	}

	@Test
	public void testIsUp() throws Exception {
		/*
		 * Achtung...das Domainobjekt heißt Thema, für REST wird daraus ein Plural!
		 */
		mockMvc.perform(get("/themas")).andExpect(status().isOk());
	}

	@Test
	public void testPersist() throws Exception {
		String json = "{ \"themaText\": \"Thema 1\" }";
		mockMvc.perform(post("/themas").content(json)).andExpect(status().isCreated());
		
		mockMvc.perform(get("/themas/search/findAll")).andExpect(status().isOk())
				.andExpect(content().contentType("application/hal+json;charset=UTF-8"));
	}

}