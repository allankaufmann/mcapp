/* tslint:disable max-line-length */
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Observable, of } from 'rxjs';
import { HttpHeaders, HttpResponse } from '@angular/common/http';

import { McappWebTestModule } from '../../../test.module';
import { BildAntwortComponent } from 'app/entities/bild-antwort/bild-antwort.component';
import { BildAntwortService } from 'app/entities/bild-antwort/bild-antwort.service';
import { BildAntwort } from 'app/shared/model/bild-antwort.model';

describe('Component Tests', () => {
    describe('BildAntwort Management Component', () => {
        let comp: BildAntwortComponent;
        let fixture: ComponentFixture<BildAntwortComponent>;
        let service: BildAntwortService;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [BildAntwortComponent],
                providers: []
            })
                .overrideTemplate(BildAntwortComponent, '')
                .compileComponents();

            fixture = TestBed.createComponent(BildAntwortComponent);
            comp = fixture.componentInstance;
            service = fixture.debugElement.injector.get(BildAntwortService);
        });

        it('Should call load all on init', () => {
            // GIVEN
            const headers = new HttpHeaders().append('link', 'link;link');
            spyOn(service, 'query').and.returnValue(
                of(
                    new HttpResponse({
                        body: [new BildAntwort(123)],
                        headers
                    })
                )
            );

            // WHEN
            comp.ngOnInit();

            // THEN
            expect(service.query).toHaveBeenCalled();
            expect(comp.bildAntworts[0]).toEqual(jasmine.objectContaining({ id: 123 }));
        });
    });
});
